<template>
  <article class="projects-list-tab">
    <template v-if="loading">
      <ProjectCardSkeleton />
      <ProjectCardSkeleton />
      <ProjectCardSkeleton />
      <ProjectCardSkeleton />
      <ProjectCardSkeleton />
      <ProjectCardSkeleton />
    </template>
    <template v-else-if="projectsList.length > 0 || isCreator">
      <template v-if="projectsList.length > 0">
        <ProjectCard
          v-for="project in projectsList"
          :key="project.id"
          class="project-card"
          :project="project"
          :route="{
            name: RouteNames.ProjectView,
            params: { projectId: project?.id }
          }"
        >
          <template #actions>
            <PermissionChecker>
              <PsiButton
                v-tooltip="project.visible ? t('forms.hide') : t('forms.show')"
                flat
                :icon="project.visible ? 'eye' : 'eye-crossed'"
                @click.prevent="onProjectChangeVisibilityClick(project, !project.visible)"
              />
              <PsiButton
                v-tooltip="t('forms.edit')"
                flat
                icon="edit"
                @click.prevent="onProjectEditClick(project)"
              />
              <PsiButton
                v-tooltip="t('forms.delete')"
                flat
                icon="trash-box"
                @click.prevent="onProjectDeleteClick(project)"
              />
            </PermissionChecker>
          </template>
        </ProjectCard>
      </template>

      <ProjectCardNew
        v-if="isCreator"
        @click="onProjectNewClick"
      />
    </template>
    <div
      v-else
      class="empty-projects-placeholder caption-regular"
    >
      {{ t('projects.list.emptyProjectsPlaceholder') }}
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
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { computed, onMounted, ref, watch } from "vue";
import { IProject, IProjectCompact } from "@/shared/types";
import { useUserStore } from "@/shared/stores/userStore.ts";
import ProjectCard from "@/modules/projects/pages/ProjectsList/components/ProjectCard/ProjectCard.vue";
import ProjectCardNew from "@/modules/projects/pages/ProjectsList/components/ProjectCardNew/ProjectCardNew.vue";
import ProjectEditDialog
  from "@/modules/projects/pages/ProjectsList/components/ProjectEditDialog/ProjectEditDialog.vue";
import ProjectDeleteDialog
  from "@/modules/projects/pages/ProjectsList/components/ProjectDeleteDialog/ProjectDeleteDialog.vue";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { ServerError } from "@/shared/utils/requests/errors.ts";
import ProjectCardSkeleton
  from "@/modules/projects/pages/ProjectsList/components/ProjectCardSkeleton/ProjectCardSkeleton.vue";
import { Result } from "@/shared/PsiUI/utils/operationResults.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import { RouteNames } from "@/router/routeNames.ts";

const toaster = useToaster();
const { t } = useI18n();
const userStore = useUserStore();
const projectsService = ProjectsServiceInstance;

const isCreator = computed(() => userStore.token);

const loading = ref(false);
const projectsList = ref<IProjectCompact[]>([]);

const projectEditDialogVisible = ref(false);
const projectEditDialogValue = ref<IProject | null>(null);

const projectDeleteDialogVisible = ref(false);
const projectDeleteDialogValue = ref<IProject | null>(null);

onMounted(async () => {
  await refresh();
});

async function refresh() {
  await Result.withLoading(() => projectsService.getProjectsList(), loading,
    {
      success: value => projectsList.value = value,
      failure: error => toaster.error(t("toaster.commonErrorHeader"), error.message)
    });
}

watch(() => userStore.locale, () => refresh());

function onProjectNewClick() {
  projectEditDialogValue.value = null;
  projectEditDialogVisible.value = true;
}

async function onProjectChangeVisibilityClick(project: IProjectCompact, visible: boolean) {
  try {
    await projectsService.changeProjectVisibility(project.id, { visible: visible });
    project.visible = visible;
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}

function onProjectEditClick(project: IProjectCompact) {
  projectEditDialogValue.value = project;
  projectEditDialogVisible.value = true;
}

function onProjectDeleteClick(project: IProjectCompact) {
  projectDeleteDialogValue.value = project;
  projectDeleteDialogVisible.value = true;
}

function onProjectCreate(project: IProjectCompact) {
  projectsList.value.unshift(project);
}

function onProjectEdit(project: IProjectCompact) {
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

<style scoped src="./ProjectsListTab.scss" />
