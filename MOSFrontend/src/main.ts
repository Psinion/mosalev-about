import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { createPinia } from "pinia";
import { OhVueIcon } from "oh-vue-icons";
import importIcons from "@/setup/icons-import.ts";
import "@/assets/styles/index.scss";
import { PsiRequestorPlugin } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { customErrorHandler } from "@/shared/utils/requests/errorHandlers.ts";
import { requestMiddlewares } from "@/shared/utils/requests/middlewaresRequest.ts";
import { mainConfig, MainConfigPlugin } from "@/main.config.ts";
import Vue3Toastify from "vue3-toastify";
import { toasterDefaultGlobalOptions } from "@/shared/PsiUI/utils/toaster.ts";
import i18n from "@/shared/utils/i18n.ts";
import { responseMiddlewares } from "@/shared/utils/requests/MiddlewaresResponse.ts";
import { markdownDirective, tooltipDirective } from "@/shared/directives";

const app = createApp(App);

app.component("VIcon", OhVueIcon);

importIcons();

app.directive("tooltip", tooltipDirective);
app.directive("markdown", markdownDirective);

app.use(MainConfigPlugin);
app.use(createPinia());
app.use(PsiRequestorPlugin, {
  baseUrl: mainConfig.appUrl,
  handleError: customErrorHandler,
  middleware: {
    request: requestMiddlewares,
    response: responseMiddlewares
  }
});
app.use(router);
app.use(Vue3Toastify, toasterDefaultGlobalOptions);
app.use(i18n);

app.mount("#app");
