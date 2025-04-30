<template>
  <ContentLayout>
    <div class="projects-list">
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
              @edit="onProjectEditClick"
            />
          </template>

          <ProjectCardNew
            v-if="isCreator"
            @click="onProjectNewClick"
          />
        </div>
        <div
          v-else
          class="empty-projects-placeholder caption-regular"
        >
          {{ t('projects.list.emptyProjectsPlaceholder') }}
        </div>
      </div>

      <ProjectDialog
        v-model:visible="projectDialogVisible"
        :project="projectDialogValue"
        @create="onProjectCreate"
        @edit="onProjectEdit"
      />
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">
import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import { useI18n } from "vue-i18n";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { computed, onMounted, ref } from "vue";
import { IProject, IProjectCompact } from "@/shared/types";
import { useUserStore } from "@/shared/stores/userStore.ts";
import ProjectCard from "@/modules/projects/pages/ProjectsList/components/ProjectCard/ProjectCard.vue";
import ProjectCardNew from "@/modules/projects/pages/ProjectsList/components/ProjectCardNew/ProjectCardNew.vue";
import ProjectDialog from "@/modules/projects/pages/ProjectsList/components/ProjectDialog/ProjectDialog.vue";

const { t } = useI18n();
const userStore = useUserStore();
const projectsService = ProjectsServiceInstance;

const isCreator = computed(() => userStore.token);

const projectsList = ref<IProjectCompact[]>([]);

const projectDialogValue = ref<IProject | null>(null);
const projectDialogVisible = ref(false);

onMounted(async () => {
  projectsList.value = await projectsService.getProjectsList();
});

function onProjectNewClick() {
  projectDialogVisible.value = true;
}

function onProjectEditClick(project: IProject) {
  projectDialogValue.value = project;
  projectDialogVisible.value = true;
}

function onProjectCreate(project: IProject) {
  projectsList.value.unshift(project);
}

function onProjectEdit(project: IProject) {
  const foundProject = projectsList.value.find(x => x.id === project.id);
  if (foundProject) {
    foundProject.title = project.title;
    foundProject.description = project.description;
  }
}
</script>

<style scoped src="./ProjectsList.scss" />
