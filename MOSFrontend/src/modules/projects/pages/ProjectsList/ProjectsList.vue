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
              @change-visibility="onProjectChangeVisibilityClick"
              @edit="onProjectEditClick"
              @delete="onProjectDeleteClick"
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

      <ProjectEditDialog
        v-model:visible="projectEditDialogVisible"
        :project="projectEditDialogValue"
        @create="onProjectCreate"
        @edit="onProjectEdit"
      />
      <ProjectDeleteDialog
        v-model:visible="projectDeleteDialogVisible"
        :project-id="projectDeleteDialogValue?.id"
        @delete="onProjectDelete"
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
import ProjectEditDialog
  from "@/modules/projects/pages/ProjectsList/components/ProjectEditDialog/ProjectEditDialog.vue";
import ProjectDeleteDialog
  from "@/modules/projects/pages/ProjectsList/components/ProjectDeleteDialog/ProjectDeleteDialog.vue";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";

const toaster = useToaster();
const { t } = useI18n();
const userStore = useUserStore();
const projectsService = ProjectsServiceInstance;

const isCreator = computed(() => userStore.token);

const projectsList = ref<IProjectCompact[]>([]);

const projectEditDialogVisible = ref(false);
const projectEditDialogValue = ref<IProject | null>(null);

const projectDeleteDialogVisible = ref(false);
const projectDeleteDialogValue = ref<IProject | null>(null);

onMounted(async () => {
  projectsList.value = await projectsService.getProjectsList();
});

function onProjectNewClick() {
  projectEditDialogVisible.value = true;
}

async function onProjectChangeVisibilityClick(project: IProject, visible: boolean) {
  try {
    await projectsService.changeProjectVisibility(project.id, { visible: visible });
    project.visible = visible;
  }
  catch (error) {
    toaster.error("Произошла ошибка при изменении видимости проекта", error);
  }
}

function onProjectEditClick(project: IProject) {
  projectEditDialogValue.value = project;
  projectEditDialogVisible.value = true;
}

function onProjectDeleteClick(project: IProject) {
  projectDeleteDialogValue.value = project;
  projectDeleteDialogVisible.value = true;
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

function onProjectDelete(projectId: number) {
  const foundIndex = projectsList.value.findIndex(x => x.id === projectId);
  if (foundIndex !== -1) {
    projectsList.value.splice(foundIndex, 1);
  }
}
</script>

<style scoped src="./ProjectsList.scss" />
