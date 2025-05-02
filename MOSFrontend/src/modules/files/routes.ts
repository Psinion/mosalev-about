import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import FilesStorage from "@/modules/files/pages/FilesStorage/FilesStorage.vue";
import { protectedPagesGuard } from "@/router/middlewares/protectedPagesGuard.ts";

export const filesRoutes: RouteRecordRaw[] = [{
  path: "/files",
  name: RouteNames.Files,
  component: FilesStorage,
  beforeEnter: protectedPagesGuard,
  meta: {
    titleCode: "pages.files"
  }
}];
