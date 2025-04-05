import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import ProjectPage from "@/modules/projects/pages/ProjectPage/ProjectPage.vue";
import ProjectsList from "@/modules/projects/pages/ProjectPage/components/ProjectsList/ProjectsList.vue";

export const projectsRoutes: RouteRecordRaw[] = [{
  path: "/projects",
  component: ProjectPage,
  children: [
    {
      path: "",
      name: RouteNames.ProjectsList,
      component: ProjectsList,
      meta: {
        titleCode: "pages.projectsList"
      }
    }
  ]
}];
