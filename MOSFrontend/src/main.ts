import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { createPinia } from "pinia";
import { OhVueIcon } from "oh-vue-icons";
import importIcons from "@/setup/icons-import.ts";
import "@/assets/styles/index.scss";
import { PsiRequestorPlugin } from "@/shared/utils/requests/requestor.ts";
import { customErrorHandler } from "@/shared/utils/requests/errorHandlers.ts";
import { requestMiddlewares } from "@/shared/utils/requests/middlewaresRequest.ts";
import { mainConfig, MainConfigPlugin } from "@/main.config.ts";
import { createI18n } from "vue-i18n";
import { en, ru } from "@/locales/locales.ts";
import Vue3Toastify from "vue3-toastify";
import { toasterDefaultGlobalOptions } from "@/shared/utils/toaster.ts";

const app = createApp(App);

const i18n = createI18n({
  legacy: false,
  locale: "ru",
  fallbackLocale: "ru",
  messages: {
    en: en,
    ru: ru
  }
});

app.component("VIcon", OhVueIcon);

importIcons();

app.use(MainConfigPlugin);
app.use(createPinia());
app.use(PsiRequestorPlugin, {
  baseUrl: mainConfig.appUrl,
  handleError: customErrorHandler,
  middleware: {
    request: requestMiddlewares
  }
});
app.use(router);
app.use(Vue3Toastify, toasterDefaultGlobalOptions);
app.use(i18n);

app.mount("#app");
