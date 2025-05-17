<template>
  <ContentLayout>
    <div class="projects-list">
      <header class="header">
        <div></div>

        <h2>{{ t('pages.projectsList') }}</h2>

        <PsiToggle
          v-model="isProjects"
          class="projects-list-toggle"
          :active-label="t('projects.list.toggleProjects')"
          :inactive-label="t('projects.list.toggleArticles')"
        />
      </header>

      <ProjectsListTab v-if="isProjects" />
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
import { useProjectsListStore } from "@/modules/projects/stores/projectsListStore.ts";

const { t } = useI18n();
const projectsListStore = useProjectsListStore();

const isProjects = ref(true);

onMounted(async () => {
  const { projectsListQuery } = projectsListStore.getQueryFromUrl();
  isProjects.value = projectsListQuery.itemsType === "projects";
});

watch(() => isProjects.value, (value) => {
  projectsListStore.updateQueryToUrl();
});
</script>

<style scoped src="./ProjectsList.scss" />
