import { defineStore } from "pinia";
import { computed, ref } from "vue";
import { TUser } from "@/shared/types";
import UsersServiceInstance from "@/shared/services/UsersService.ts";
import { RouteNames } from "@/router/routeNames.ts";
import { useRouter } from "vue-router";

const MOS_TOKEN_STORAGE_KEY = "token";

export const useUserStore = defineStore("user", () => {
  const router = useRouter();

  const user = ref<TUser | null>();
  const authToken = ref<string | null>(localStorage.getItem(MOS_TOKEN_STORAGE_KEY));

  const token = computed(() => authToken.value);
  const isAuthenticated = computed(() => authToken.value != null);

  async function login(
    username: string,
    password: string
  ) {
    try {
      const response = await UsersServiceInstance.authenticate({
        userName: username,
        password: password
      });

      if (response.token) {
        setToken(response.token);
        user.value = response.user;

        await router.push({ name: RouteNames.IndexView });
      }
    }
    catch (error) {
      throw error;
    }
  }

  function setToken(token: string | null) {
    if (token) {
      localStorage.setItem(MOS_TOKEN_STORAGE_KEY, token);
      authToken.value = token;
    }
    else {
      localStorage.removeItem(MOS_TOKEN_STORAGE_KEY);
      authToken.value = null;
    }
  }

  return {
    user,
    token,
    isAuthenticated,

    login
  };
});
