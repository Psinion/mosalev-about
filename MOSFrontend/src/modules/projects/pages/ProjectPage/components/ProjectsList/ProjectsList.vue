<template>
  <article class="projects-list">
    <div class="actions" />
    <div class="content">
      <h2>{{ t('pages.projectsList') }}</h2>
      <div
        class="projects"
      />
      <div class="empty-projects-placeholder caption-regular">
        {{ t('projects.emptyProjectsPlaceholder') }}
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { onMounted } from "vue";

const { t } = useI18n();
const projectsService = ProjectsServiceInstance;

onMounted(async () => {
  await projectsService.getProjectsList();
});

</script>

<style scoped src="./ProjectsList.scss" />
