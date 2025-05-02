import { RouteRecordRaw } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import ProjectsList from "@/modules/projects/pages/ProjectsList/ProjectsList.vue";
import ProjectView from "@/modules/projects/pages/ProjectView/ProjectView.vue";
import ArticleEdit from "@/modules/projects/pages/ArticleEdit/ArticleEdit.vue";
import ArticleView from "@/modules/projects/pages/ArticleView/ArticleView.vue";
import { protectedPagesGuard } from "@/router/middlewares/protectedPagesGuard.ts";

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
},
{
  path: "/articles/:articleId?",
  name: RouteNames.ArticleView,
  props: route => ({ articleId: Number(route.params.articleId ?? 0) }),
  component: ArticleView,
  meta: {
    titleCode: "pages.articleView"
  }
},
{
  path: "/articles/edit/:articleId?",
  name: RouteNames.ArticleEdit,
  props: route => ({ articleId: Number(route.params.articleId ?? 0) }),
  component: ArticleEdit,
  beforeEnter: protectedPagesGuard,
  meta: {
    titleCode: "pages.articleEdit"
  }
}];
