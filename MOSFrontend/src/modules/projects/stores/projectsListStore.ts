import { defineStore } from "pinia";
import { useRoute, useRouter } from "vue-router";
import { ref } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import { checkPositiveNumber } from "@/shared/utils/helpers";
import { PsiDictionary } from "@/shared/PsiUI/types/base.ts";

export type TProjectsItemType = "projects" | "articles";
export type TProjectsListQuery = {
  itemsType: TProjectsItemType;
};
export type TProjectsProjectsTabQuery = {
  page: number;
};
export type TProjectsArticlesTabQuery = {
  page: number;
};

export const useProjectsListStore = defineStore("projectsList", () => {
  const route = useRoute();
  const router = useRouter();

  const projectsListQuery = ref <TProjectsListQuery>({
    itemsType: "projects"
  });

  const projectsProjectsTabQuery = ref <TProjectsProjectsTabQuery>({
    page: 1
  });

  const projectsArticlesTabQuery = ref <TProjectsArticlesTabQuery>({
    page: 1
  });

  const getQueryFromUrl = () => {
    const projectsList = projectsListQuery.value;
    const projectsProjectsTab = projectsProjectsTabQuery.value;
    const projectsArticlesTab = projectsArticlesTabQuery.value;

    const itemsTypeString = route.query["itemsType"] as TProjectsItemType;
    if (itemsTypeString && (itemsTypeString === "projects" || itemsTypeString === "articles")) {
      projectsList.itemsType = itemsTypeString;
    }

    if (projectsList.itemsType === "projects") {
      const pageString = route.query["page"] as string;
      if (checkPositiveNumber(pageString)) {
        projectsProjectsTab.page = +pageString;
        console.trace(pageString);
      }
    }
    else {
      const pageString = route.query["page"] as string;
      if (checkPositiveNumber(pageString)) {
        projectsArticlesTab.page = +pageString;
      }
    }

    return {
      projectsListQuery: projectsList,
      projectsProjectsTabQuery: projectsProjectsTab,
      projectsArticlesTabQuery: projectsArticlesTab
    };
  };

  const updateQueryToUrl = async () => {
    const projectsList = projectsListQuery.value;
    const projectsProjectsTab = projectsProjectsTabQuery.value;
    const projectsArticlesTab = projectsArticlesTabQuery.value;

    const childQuery: PsiDictionary<string> = {};

    if (projectsList.itemsType === "projects") {
      if (projectsProjectsTab.page !== 1) {
        childQuery["page"] = projectsProjectsTab.page.toString();
      }
    }
    else {
      if (projectsArticlesTab.page !== 1) {
        childQuery["page"] = projectsArticlesTab.page.toString();
      }
    }

    await router.replace({
      name: RouteNames.ProjectsList,
      query: {
        itemsType: projectsList.itemsType,
        ...childQuery
      }
    });
  };

  return {
    projectsListQuery,
    projectsProjectsTabQuery,
    projectsArticlesTabQuery,

    getQueryFromUrl,
    updateQueryToUrl
  };
});
