import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import LoginPage from "./pages/LoginPage/LoginPage.vue";
import IndexPage from "@/modules/main/pages/IndexPage/IndexPage.vue";

export const publicMainRoutes: RouteRecordRaw[] = [{
  path: "/login",
  name: RouteNames.Login,
  component: LoginPage,
  meta: {
    titleCode: "pages.login"
  }
},
{
  path: "",
  name: RouteNames.Index,
  component: IndexPage,
  meta: {
    titleCode: "pages.index"
  }
}];

export const protectedMainRoutes: RouteRecordRaw[] = [
];
