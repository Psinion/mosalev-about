import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import ResumePage from "@/modules/resume/pages/ResumePage/ResumePage.vue";
import ResumeEdit from "@/modules/resume/pages/ResumePage/components/ResumeEdit/ResumeEdit.vue";
import ResumeView from "@/modules/resume/pages/ResumePage/components/ResumeView/ResumeView.vue";
import ResumeList from "@/modules/resume/pages/ResumePage/components/ResumeList/ResumeList.vue";
import { protectedPagesGuard } from "@/router/middlewares/protectedPagesGuard.ts";

export const resumeRoutes: RouteRecordRaw[] = [{
  path: "/resume",
  component: ResumePage,
  children: [
    {
      path: ":resumeId?",
      name: RouteNames.ResumeView,
      component: ResumeView,
      props: route => ({ resumeId: Number(route.params.resumeId) }),
      meta: {
        titleCode: "pages.resumeView"
      }
    },
    {
      path: "list",
      name: RouteNames.ResumeList,
      component: ResumeList,
      beforeEnter: protectedPagesGuard,
      meta: {
        titleCode: "pages.resumeList"
      }
    },
    {
      path: "edit/:resumeId?",
      name: RouteNames.ResumeEdit,
      props: route => ({ resumeId: Number(route.params.resumeId) }),
      beforeEnter: protectedPagesGuard,
      component: ResumeEdit,
      meta: {
        titleCode: "pages.resumeEdit"
      }
    }
  ]
}];
