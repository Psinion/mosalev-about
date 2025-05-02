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
import { ref } from "vue";
import PsiToggle from "@/shared/PsiUI/components/PsiToggle/PsiToggle.vue";
import ProjectsListTab from "@/modules/projects/pages/ProjectsList/tabs/ProjectsListTab/ProjectsListTab.vue";
import ArticlesListTab from "@/modules/projects/pages/ProjectsList/tabs/ArticlesListTab/ArticlesListTab.vue";

const { t } = useI18n();

const isProjectsList = ref(true);
</script>

<style scoped src="./ProjectsList.scss" />
