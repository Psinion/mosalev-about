import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import LoginPage from "./pages/LoginPage/LoginPage.vue";
import IndexView from "@/modules/main/pages/IndexPage/components/IndexView/IndexView.vue";
import IndexPage from "@/modules/main/pages/IndexPage/IndexPage.vue";
import AboutPage from "@/modules/main/pages/AboutPage/AboutPage.vue";

export const publicMainRoutes: RouteRecordRaw[] = [{
  path: "/login",
  name: RouteNames.Login,
  component: LoginPage,
  meta: {
    title: "Авторизация"
  }
}];

export const protectedMainRoutes: RouteRecordRaw[] = [{
  path: "",
  name: RouteNames.IndexView,
  component: IndexPage,
  meta: {
    title: "Главная"
  }
},
{
  path: "/edit",
  name: RouteNames.IndexEdit,
  component: IndexView,
  meta: {
    title: "Редактирование главной"
  }
},
{
  path: "/about",
  name: RouteNames.AboutView,
  component: AboutPage,
  meta: {
    title: "Обо мне"
  }
}
];
