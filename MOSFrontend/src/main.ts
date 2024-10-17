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

const app = createApp(App);

app.component("VIcon", OhVueIcon);

importIcons();

app.use(createPinia());
app.use(PsiRequestorPlugin, {
  baseUrl: "https://localhost:8081/",
  handleError: customErrorHandler,
  middleware: {
    request: requestMiddlewares
  }
});
app.use(router);

app.mount("#app");
