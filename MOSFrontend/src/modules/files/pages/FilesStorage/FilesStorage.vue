<template>
  <ContentLayout>
    <div class="files-storage">
      <h2> {{ t('pages.files') }}</h2>

      <div class="content">
        <template v-if="loading">
        </template>
        <template v-else>
          <div class="storage-info-graphic caption-regular">
            <div class="total-size hint-regular">
              {{ totalSizeString }}
            </div>

            <div class="graphic">
              <div class="occupied">
                <div
                  v-tooltip="{
                    text: 'Занято ' + occupiedSizeString,
                    width: '170px',
                  }"
                  class="line"
                />
                <div class="hint-regular">
                  {{ occupiedSizeString }}
                </div>
              </div>
              <div class="free-space">
                <div
                  v-tooltip="{
                    text: 'Свободно ' + freeSpaceString,
                    width: '170px',
                  }"
                  :style="{ width: freeSpaceGraphicPercent + 'px'}"
                  class="line"
                />
                <div class="hint-regular">
                  {{ freeSpaceString }}
                </div>
              </div>
            </div>
          </div>

          <div class="files">
            <PsiFileUpload
              class="file-upload-area"
              :file-size-max="1024 * 1024 * 5"
              file-possible-types=".jpg.jpeg.png"
              area
              @files-upload="onFilesUpload($event)"
              @incorrect-file-type="uploadedFileIncorrectType"
              @file-too-large="uploadedFileTooLarge"
            >
              <div class="caption-regular">
                Перетащите файлы для загрузки на сервер
              </div>
            </PsiFileUpload>

            <div class="files-list">
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
                  @delete="onFileDeleteClick"
                />

                <PsiPagination
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
                {{ t('projects.view.emptyArticlesPlaceholder') }}
              </div>
            </div>
          </div>
        </template>
      </div>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">
import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import { computed, onMounted, ref } from "vue";
import { IStorageInfo, IUploadedFile, IUploadedFilesPagination } from "@/shared/types";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import FilesServiceInstance from "@/shared/services/FilesService.ts";
import PsiFileUpload from "@/shared/PsiUI/components/PsiFileUpload/PsiFileUpload.vue";
import ArticleCardSkeleton from "@/modules/projects/shared/ArticleCardSkeleton/ArticleCardSkeleton.vue";
import PsiPagination from "@/shared/PsiUI/components/PsiPagination/PsiPagination.vue";
import FileCard from "@/modules/files/shared/FileCard/FileCard.vue";
import { fileSize2Text } from "@/modules/files/utils/files.ts";

const router = useRouter();
const toaster = useToaster();
const { t } = useI18n();
const filesService = FilesServiceInstance;

const loading = ref(true);
const currentStorageInfo = ref<IStorageInfo | null>(null);

const limit = 5;
const currentPage = ref(1);
const paginationTotal = ref(0);

const filesLoading = ref(true);
const filesList = ref<IUploadedFilesPagination | null>(null);

const freeSpaceString = computed(() => fileSize2Text(currentStorageInfo.value.freeSpace));
const totalSizeString = computed(() => fileSize2Text(currentStorageInfo.value.totalSize));

const freeSpaceGraphicPercent = computed(() => {
  const info = currentStorageInfo.value;
  const percent = info.freeSpace / info.totalSize * 100;
  return percent;
});
const occupiedSizeString = computed(() =>
  fileSize2Text(currentStorageInfo.value.totalSize - currentStorageInfo.value.freeSpace));

onMounted(async () => {
  try {
    loading.value = true;
    currentStorageInfo.value = await filesService.getStorageInfo();
    loading.value = false;

    await refreshFiles(0);
  }
  catch (error) {
    if (error instanceof ServerError) {
      if (error.statusCode === 404) {
        await router.push({ name: RouteNames.Error404 });
        return;
      }
      toaster.error(error.header, error.message);
    }
  }
});

async function refreshFiles(offset?: number) {
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
}

async function onFilesUpload(files: FileList) {
  for (const file of Array.from(files)) {
    try {
      const uploadedFile = await filesService.createFile(file);
    }
    catch (error) {
      toaster.error("Произошла ошибка при прикреплении файла", error);
    }
  }
}

function uploadedFileTooLarge() {
  toaster.neutral("Файл слишком большой.");
}

function uploadedFileIncorrectType() {
  toaster.neutral("Не подходящий формат файла.");
}

function onFileDeleteClick(value: IUploadedFile) {

}
</script>

<style scoped src="./FilesStorage.scss" />
