import { defineStore } from "pinia";
import { computed, ref } from "vue";
import { TUser } from "@/shared/types";
import { RouteNames } from "@/router/routeNames.ts";
import { useRoute, useRouter } from "vue-router";
import { AppLocale } from "@/shared/enums/common.ts";
import { setPageTitle } from "@/shared/utils/helpers/helpers.ts";
import { useI18n } from "vue-i18n";
import dayjs from "dayjs";
import UsersServiceInstance from "@/shared/services/UsersService.ts";

const MOS_TOKEN_STORAGE_KEY = "token";
const MOS_LOCALE_STORAGE_KEY = "locale";

export const useUserStore = defineStore("user", () => {
  const route = useRoute();
  const router = useRouter();
  const { locale: i18nLocale, t } = useI18n();

  const user = ref<TUser | null>();
  const authToken = ref<string | null>(localStorage.getItem(MOS_TOKEN_STORAGE_KEY));
  const localeInternal = ref<AppLocale>(initializeLocale());

  const locale = computed<AppLocale>(() => {
    return localeInternal.value;
  });
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

        await router.push({ name: RouteNames.Index });
      }
    }
    catch (error) {
      throw error;
    }
  }

  async function logout() {
    setToken(null);
    user.value = null;
    await router.push({ name: RouteNames.Login });
  }

  async function checkLogin() {
    try {
      const response = await UsersServiceInstance.verify();
      user.value = response.user;
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

  function setLocale(locale: AppLocale) {
    localeInternal.value = locale;
    const localeString = locale === AppLocale.ru ? "ru" : "en";
    i18nLocale.value = localeString;
    dayjs.locale(localeString);
    localStorage.setItem(MOS_LOCALE_STORAGE_KEY, locale.toString());

    const titleCode = route.meta["titleCode"] as string;
    setPageTitle(t(titleCode));
  }

  function initializeLocale(): AppLocale {
    const locale = localStorage.getItem(MOS_LOCALE_STORAGE_KEY);
    if (locale) {
      const localeNumber = Number(locale);
      if (localeNumber <= AppLocale.en) {
        i18nLocale.value = localeNumber === AppLocale.ru ? "ru" : "en";
        return localeNumber;
      }
    }

    return AppLocale.ru;
  }

  return {
    user,
    token,
    isAuthenticated,
    locale,

    login,
    logout,
    checkLogin,
    setLocale
  };
});
