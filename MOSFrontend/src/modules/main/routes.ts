import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "../../router/routeNames.ts";
import IndexPage from "./pages/IndexPage/IndexPage.vue";
import LoginPage from "./pages/LoginPage/LoginPage.vue";

export const mainRoutes: RouteRecordRaw[] = [{
    path: "/",
    name: RouteNames.Index,
    component: IndexPage,
    meta: {
        title: "Главная"
    }
},
{
    path: "/login",
    name: RouteNames.Login,
    component: LoginPage,
    meta: {
        title: "Авторизация"
    }
}];
