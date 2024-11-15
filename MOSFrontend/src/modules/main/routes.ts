import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import LoginPage from "./pages/LoginPage/LoginPage.vue";
import IndexPage from "@/modules/main/pages/IndexPage/IndexPage.vue";
import AboutPage from "@/modules/main/pages/AboutPage/AboutPage.vue";

export const publicMainRoutes: RouteRecordRaw[] = [{
  path: "/login",
  name: RouteNames.Login,
  component: LoginPage,
  meta: {
    title: "Авторизация"
  }
},
{
  path: "",
  name: RouteNames.Index,
  component: IndexPage,
  meta: {
    title: "Главная"
  }
},
{
  path: "/about",
  name: RouteNames.AboutView,
  component: AboutPage,
  meta: {
    title: "Обо мне"
  }
}];

export const protectedMainRoutes: RouteRecordRaw[] = [
];
