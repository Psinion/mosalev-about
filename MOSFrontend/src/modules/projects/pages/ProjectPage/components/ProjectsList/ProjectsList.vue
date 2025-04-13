<template>
  <article class="projects-list">
    <div class="actions" />
    <div class="content">
      <h2>{{ t('pages.projectsList') }}</h2>
      <div
        v-if="projectsList.length > 0 || isCreator"
        class="projects"
      >
        <template v-for="project in projectsList">
          <ProjectCard
            v-if="projectsList.length > 0"
            :key="project.id"
            :project="project"
          />
        </template>

        <ProjectCardNew
          v-if="isCreator"
          @click="onProjectNewClick"
        ></ProjectCardNew>
      </div>
      <div
        v-else
        class="empty-projects-placeholder caption-regular"
      >
        {{ t('projects.list.emptyProjectsPlaceholder') }}
      </div>
    </div>

    <ProjectDialog v-model:visible="projectDialogVisible" />
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { computed, onMounted, ref } from "vue";
import { IProjectCompact } from "@/shared/types";
import { useUserStore } from "@/shared/stores/userStore.ts";
import ProjectCardNew
  from "@/modules/projects/pages/ProjectPage/components/ProjectsList/ProjectCardNew/ProjectCardNew.vue";
import ProjectDialog
  from "@/modules/projects/pages/ProjectPage/components/ProjectsList/ProjectDialog/ProjectDialog.vue";
import ProjectCard from "@/modules/projects/pages/ProjectPage/components/ProjectsList/ProjectCard/ProjectCard.vue";

const { t } = useI18n();
const userStore = useUserStore();
const projectsService = ProjectsServiceInstance;

const isCreator = computed(() => userStore.token);

const projectsList = ref<IProjectCompact[]>([]);
const projectDialogVisible = ref(false);

onMounted(async () => {
  projectsList.value = await projectsService.getProjectsList();
});

function onProjectNewClick() {
  projectDialogVisible.value = true;
}
</script>

<style scoped src="./ProjectsList.scss" />
