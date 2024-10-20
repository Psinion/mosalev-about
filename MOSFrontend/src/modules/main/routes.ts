import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import LoginPage from "./pages/LoginPage/LoginPage.vue";
import IndexPage from "@/modules/main/pages/IndexPage/IndexPage.vue";
import IndexView from "@/modules/main/pages/IndexPage/components/IndexView/IndexView.vue";

export const mainRoutes: RouteRecordRaw[] = [{
  path: "/",
  component: IndexPage,
  children: [{
    path: "",
    name: RouteNames.IndexView,
    component: IndexView,
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
  }
  ]
},
{
  path: "/login",
  name: RouteNames.Login,
  component: LoginPage,
  meta: {
    title: "Авторизация"
  }
}];
