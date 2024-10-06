import type { IUserAuthResponseDto } from "@/dtos/IUserAuthResponseDto";
import type { IUserAuth } from "@/entities/IUserAuth";
import { defineStore } from "pinia";
import router from "@/router";
import { computed, ref } from "vue";
import { mainRequestor } from "../../utils/requestor";
import type { UserPermissionDto } from "@/dtos/userPermissionDto";
import { UserPermissions } from "./userPermissions";
import type { User } from "@/models/user";

export const useUserStore = defineStore("user", () => {
  const user = ref<User | null>(
    JSON.parse(localStorage.getItem("user") as string) as User
  );
  const token = ref<string | null>(localStorage.getItem("token"));
  const returnUrl = ref<string | null>(null);
  const userPermissions = ref<Record<string, boolean | undefined> | null>(null);

  const isAuthenticated = computed(() => token.value != null);

  async function login(
    username: string,
    password: string,
    rememberMe: boolean
  ) {
    const endpoint = "users/authenticate";

    const requestData: IUserAuth = {
      username: username,
      password: password,
      rememberMe: rememberMe
    };

    try {
      const response = await mainRequestor.post<IUserAuthResponseDto>(
        `${endpoint}`,
        "POST",
        {
          body: requestData
        }
      );

      const rUser = response.user;
      user.value = {
        adAccount: rUser.adAccount,
        firstName: rUser.firstName,
        middleName: rUser.middleName,
        lastName: rUser.lastName,
        fio: rUser.fio
      };
      localStorage.setItem("user", JSON.stringify(user.value));

      token.value = response.token;
      localStorage.setItem("token", token.value);

      router.push(returnUrl.value ?? { name: "Index" });
    }
    catch (error) {
      throw error;
    }
  }

  const logout = () => {
    user.value = null;
    token.value = null;
    localStorage.removeItem("user");
    localStorage.removeItem("token");
    router.push({ name: "Login" });
  };

  const fetchUserPermissions = async (): Promise<void> => {
    if (userPermissions.value) {
      return;
    }

    if (isAuthenticated.value) {
      const endpoint = "users/permissions";

      try {
        const response = await mainRequestor.post<UserPermissionDto>(
          `${endpoint}`,
          "POST",
          {
            needErrorHandle: false
          }
        );

        // TODO: Написать юнит тест для проверки правильности прав на клиенте и сервере.
        const permissions = response.permissions;

        userPermissions.value = {};

        for (const perm in UserPermissions) {
          userPermissions.value[perm] = false;
        }
        userPermissions.value[UserPermissions.LoggedIn] = true;

        for (const perm of permissions) {
          userPermissions.value[perm] = true;
        }
      }
      catch (error) {
        console.log(error);
      }
    }
  };

  const checkPermission = (permission: UserPermissions | string): boolean => {
    if (!userPermissions.value) {
      return false;
    }

    return userPermissions.value[permission] ?? false;
  };

  const checkPermissions = (
    permissions: UserPermissions[] | string[]
  ): boolean => {
    if (!userPermissions.value) {
      return false;
    }

    return permissions.some(permission =>
      userPermissions.value ? userPermissions.value[permission] : false
    );
  };

  return {
    user,
    token,
    returnUrl,
    isAuthenticated,
    login,
    logout,
    fetchUserPermissions,
    checkPermission,
    checkPermissions
  };
});
