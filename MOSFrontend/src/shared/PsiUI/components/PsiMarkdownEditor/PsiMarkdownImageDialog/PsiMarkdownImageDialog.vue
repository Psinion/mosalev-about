<template>
  <PsiDialog
    :visible="visible"
    size="M"
    hide-when-click-outside
    @update:visible="$emit('update:visible', $event)"
    @open="refreshFiles"
  >
    <template #header>
      {{ t('psiUi.psiMarkdownEditor.title') }}
    </template>

    <div class="psi-markdown-image-editor">
      <div class="foreign-image-block">
        <PsiInput
          v-model="foreignUrlInput"
          class="foreign-image-input"
          :label="t('psiUi.psiMarkdownEditor.foreignImageInput')"
        />
        <PsiButton
          class="foreign-image-button"
          :disabled="!canInsertForeignUrl"
          @click="onForeignUrlClick"
        >
          {{ t('psiUi.psiDialog.confirmButton') }}
        </PsiButton>
      </div>

      <div
        v-if="filesLoading"
        class="images"
      >
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
      </div>
      <div
        v-else-if="filesList?.items && filesList.items.length"
        class="images"
      >
        <FileCard
          v-for="file in filesList.items"
          :key="file.id"
          :file="file"
          readonly
          @click="onImageClick(file)"
        />

        <PsiPagination
          ref="paginationRef"
          v-model:current-page="currentPage"
          :limit="limit"
          :total="paginationTotal"
          class="pagination"
          @select-page="refreshFiles"
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
import { FileKind, IUploadedFile, IUploadedFilesPagination } from "@/shared/types";
import PsiPagination from "@/shared/PsiUI/components/PsiPagination/PsiPagination.vue";
import ArticleCardSkeleton from "@/modules/projects/shared/ArticleCardSkeleton/ArticleCardSkeleton.vue";
import FileCard from "@/modules/files/shared/FileCard/FileCard.vue";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { Result } from "@/shared/PsiUI/utils/operationResults.ts";

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
const filesService = FilesServiceInstance;

const foreignUrlInput = ref<string | undefined>(undefined);

const limit = 5;
const currentPage = ref(1);
const paginationTotal = ref(0);

const filesLoading = ref(true);
const filesList = ref<IUploadedFilesPagination | null>(null);

const canInsertForeignUrl = computed(() => foreignUrlInput.value && foreignUrlInput.value.length > 0);

const refreshFiles = async (offset?: number) => {
  foreignUrlInput.value = undefined;

  await Result.withLoading(() =>
    filesService.getFiles({
      limit: limit,
      offset: offset,
      fileKind: FileKind.Image
    })
  , filesLoading, {
    success: (value) => {
      filesList.value = value;
      paginationTotal.value = value.totalCount;
    },
    failure: (error) => {
      toaster.error("toaster.commonErrorHeader", error.message);
    }
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

<style scoped src="./PsiMarkdownImageDialog.scss" />
