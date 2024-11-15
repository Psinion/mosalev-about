import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import ResumeViewPage from "@/modules/resume/pages/ResumeViewPage/ResumeViewPage.vue";
import { protectedPagesGuard } from "@/router/middlewares/protectedPagesGuard.ts";

export const resumeRoutes: RouteRecordRaw[] = [{
  path: "/resume",
  name: RouteNames.ResumeView,
  component: ResumeViewPage,
  beforeEnter: protectedPagesGuard,
  meta: {
    title: "Резюме"
  }
}];
