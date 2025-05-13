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
      <template v-if="filesLoading">
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
      </template>
      <template v-else-if="filesList?.items && filesList.items.length">
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
      </template>
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
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { ref } from "vue";
import { IUploadedFile, IUploadedFilesPagination } from "@/shared/types";
import PsiPagination from "@/shared/PsiUI/components/PsiPagination/PsiPagination.vue";
import ArticleCardSkeleton from "@/modules/projects/shared/ArticleCardSkeleton/ArticleCardSkeleton.vue";
import FileCard from "@/modules/files/shared/FileCard/FileCard.vue";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "select": (file: IUploadedFile) => true
});

const toaster = useToaster();
const { t } = useI18n();
const filesService = FilesServiceInstance;

const limit = 5;
const currentPage = ref(1);
const paginationTotal = ref(0);

const filesLoading = ref(true);
const filesList = ref<IUploadedFilesPagination | null>(null);

const refreshFiles = async (offset?: number) => {
  try {
    filesLoading.value = true;

    filesList.value = await filesService.getFiles({
      limit: limit,
      offset: offset
    });
    paginationTotal.value = filesList.value.totalCount;

    filesLoading.value = false;
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
};

const onImageClick = (file: IUploadedFile) => {
  emit("select", file);
  emit("update:visible", false);
};
</script>

<style scoped src="./PsiMarkdownImageDialog.scss" />
