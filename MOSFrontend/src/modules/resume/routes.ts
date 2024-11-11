import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import ResumeViewPage from "@/modules/resume/pages/ResumeViewPage/ResumeViewPage.vue";

export const resumeRoutes: RouteRecordRaw[] = [{
  path: "/resume",
  name: RouteNames.ResumeView,
  component: ResumeViewPage,
  meta: {
    title: "Резюме"
  }
}];
