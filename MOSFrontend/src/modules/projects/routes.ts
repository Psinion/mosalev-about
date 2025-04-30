import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import ProjectsList from "@/modules/projects/pages/ProjectsList/ProjectsList.vue";
import ProjectView from "@/modules/projects/pages/ProjectView/ProjectView.vue";

export const projectsRoutes: RouteRecordRaw[] = [{
  path: "/projects",
  name: RouteNames.ProjectsList,
  component: ProjectsList,
  meta: {
    titleCode: "pages.projectsList"
  }
},
{
  path: "/projects/:projectId",
  name: RouteNames.ProjectView,
  props: route => ({ projectId: Number(route.params.projectId ?? 0) }),
  component: ProjectView,
  meta: {
    titleCode: "pages.projectView"
  }
}];
