<template>
  <PsiDialog
    :visible="visible"
    size="M"
    hide-when-click-outside
    @update:visible="$emit('update:visible', $event)"
    @open="refreshFiles"
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
        />
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
import FilesServiceInstance from "@/shared/services/FilesService.ts";
import { computed, ref } from "vue";
import { FileKind, IProjectCompact, IUploadedFile, IUploadedFilesPagination } from "@/shared/types";
import PsiPagination from "@/shared/PsiUI/components/PsiPagination/PsiPagination.vue";
import ArticleCardSkeleton from "@/modules/projects/shared/ArticleCardSkeleton/ArticleCardSkeleton.vue";
import FileCard from "@/modules/files/shared/FileCard/FileCard.vue";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { Result } from "@/shared/PsiUI/utils/operationResults.ts";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import ProjectCardSkeleton
  from "@/modules/projects/pages/ProjectsList/components/ProjectCardSkeleton/ProjectCardSkeleton.vue";
import ProjectCard from "@/modules/projects/pages/ProjectsList/components/ProjectCard/ProjectCard.vue";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "select": (file: IUploadedFile) => true,
  "insertForeignUrl": (url: string) => true
});

const toaster = useToaster();
const { t } = useI18n();
const projectsService = ProjectsServiceInstance;

const foreignUrlInput = ref<string | undefined>(undefined);

const limit = 5;
const currentPage = ref(1);
const paginationTotal = ref(0);

const projectsLoading = ref(true);
const projectsList = ref<IProjectCompact[]>([]);

const canInsertForeignUrl = computed(() => foreignUrlInput.value && foreignUrlInput.value.length > 0);

const refreshFiles = async () => {
  await Result.withLoading(() => projectsService.getProjectsList(), projectsLoading,
    {
      success: value => projectsList.value = value,
      failure: error => toaster.error(t("toaster.commonErrorHeader"), error.message)
    });
};

const onImageClick = (file: IUploadedFile) => {
  emit("select", file);
  emit("update:visible", false);
};

const onForeignUrlClick = () => {
  emit("insertForeignUrl", foreignUrlInput.value);
  emit("update:visible", false);
};
</script>

<style scoped src="./ChangeArticleProjectDialog.scss" />
