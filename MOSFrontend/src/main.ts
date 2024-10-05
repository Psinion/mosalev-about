import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import { createPinia } from "pinia";
import { OhVueIcon } from "oh-vue-icons";
import importIcons from "@/setup/icons-import.ts";
import "@/assets/styles/index.scss";

const app = createApp(App);

app.component("VIcon", OhVueIcon);

importIcons();

app.use(router);
app.use(createPinia);

app.mount("#app");
