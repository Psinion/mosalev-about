<template>
  <ContentLayout>
    <div class="projects-list">
      <header class="header">
        <div></div>

        <h2>{{ t('pages.projectsList') }}</h2>

        <PsiToggle
          v-model="isProjectsList"
          class="projects-list-toggle"
          :active-label="t('projects.list.toggleProjects')"
          :inactive-label="t('projects.list.toggleArticles')"
        />
      </header>

      <ProjectsListTab v-if="isProjectsList" />
      <ArticlesListTab v-else />
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">
import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import { useI18n } from "vue-i18n";
import { onMounted, ref, watch } from "vue";
import PsiToggle from "@/shared/PsiUI/components/PsiToggle/PsiToggle.vue";
import ProjectsListTab from "@/modules/projects/pages/ProjectsList/tabs/ProjectsListTab/ProjectsListTab.vue";
import ArticlesListTab from "@/modules/projects/pages/ProjectsList/tabs/ArticlesListTab/ArticlesListTab.vue";
import { useRoute, useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";

const route = useRoute();
const router = useRouter();
const { t } = useI18n();

const isProjectsList = ref(true);

onMounted(() => {
  const isProjectsQuery = route.query["isProjects"];
  if (isProjectsQuery) {
    isProjectsList.value = +isProjectsQuery !== 0;
  }
});

watch(() => isProjectsList.value, (value) => {
  router.replace({
    name: RouteNames.ProjectsList,
    query: {
      isProjects: value ? 1 : 0
    }
  });
}, { immediate: true });
</script>

<style scoped src="./ProjectsList.scss" />
