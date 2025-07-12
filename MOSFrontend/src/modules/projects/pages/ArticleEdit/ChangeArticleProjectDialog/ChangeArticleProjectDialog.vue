<template>
  <PsiDialog
    :visible="visible"
    size="M"
    hide-when-click-outside
    @update:visible="$emit('update:visible', $event)"
    @open="refreshProjects"
  >
    <template #header>
      {{ t('articles.changeArticleProjectDialog.title') }}
    </template>

    <div class="change-article-project-dialog">
      <div
        v-if="projectsLoading"
        class="projects"
      >
        <ProjectCardSkeleton />
        <ProjectCardSkeleton />
        <ProjectCardSkeleton />
        <ProjectCardSkeleton />
        <ProjectCardSkeleton />
        <ProjectCardSkeleton />
      </div>
      <div
        v-else-if="projectsList.length > 0"
        class="projects"
      >
        <ProjectCard
          v-for="project in projectsList"
          :key="project.id"
          :project="project"
          :disabled="project.id === projectId"
          @click.prevent="changeProject(project)"
        >
          <template #actions>
            <PsiButton
              v-if="project.id === projectId"
              v-tooltip="t('articles.changeArticleProjectDialog.unpinButton')"
              flat
              icon="pin-filled"
            />
          </template>
        </ProjectCard>
      </div>
      <div
        v-else
        class="empty-files-placeholder caption-regular"
      >
        {{ t('files.storage.emptyFilesPlaceholder') }}
      </div>
    </div>

    <template #footer>
      <div></div>
    </template>
  </PsiDialog>
</template>

<script setup lang="ts">
import PsiDialog from "@/shared/PsiUI/components/PsiDialog/PsiDialog.vue";
import { useI18n } from "vue-i18n";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { ref } from "vue";
import { IProjectCompact } from "@/shared/types";
import { Result } from "@/shared/PsiUI/utils/operationResults.ts";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import ProjectCardSkeleton
  from "@/modules/projects/pages/ProjectsList/components/ProjectCardSkeleton/ProjectCardSkeleton.vue";
import ProjectCard from "@/modules/projects/pages/ProjectsList/components/ProjectCard/ProjectCard.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import ArticlesServiceInstance from "@/shared/services/ArticlesService.ts";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  articleId: {
    type: Number,
    default: 0
  },
  projectId: {
    type: Number,
    default: 0
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "select": (project: IProjectCompact | null) => true
});

const toaster = useToaster();
const { t } = useI18n();
const projectsService = ProjectsServiceInstance;
const articlesService = ArticlesServiceInstance;

const projectsLoading = ref(true);
const projectsList = ref<IProjectCompact[]>([]);

const refreshProjects = async () => {
  await Result.withLoading(() => projectsService.getProjectsList(), projectsLoading,
    {
      success: value => projectsList.value = value,
      failure: error => toaster.error(t("toaster.commonErrorHeader"), error.message)
    });
};

const changeProject = async (project: IProjectCompact) => {
  const isChanged = props.projectId !== project.id;

  (await articlesService.changeArticleProject(props.articleId, {
    projectId: isChanged ? project.id : null
  }))
    .match(() => {
      emit("select", isChanged ? project : null);
    });
};
</script>

<style scoped src="./ChangeArticleProjectDialog.scss" />
