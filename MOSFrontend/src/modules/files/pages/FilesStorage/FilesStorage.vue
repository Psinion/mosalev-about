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
import { IStorageInfo } from "@/shared/types";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import FilesServiceInstance from "@/shared/services/FilesService.ts";
import PsiFileUpload from "@/shared/PsiUI/components/PsiFileUpload/PsiFileUpload.vue";

const router = useRouter();
const toaster = useToaster();
const { t } = useI18n();
const filesService = FilesServiceInstance;

const SIZES_TABLE = [
  { name: "Кб", size: 1024 },
  { name: "Мб", size: 1024 * 1024 },
  { name: "Гб", size: 1024 * 1024 * 1024 },
  { name: "Тб", size: 1024 * 1024 * 1024 * 1024 }
];

const loading = ref(true);
const currentStorageInfo = ref<IStorageInfo | null>(null);

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

function fileSize2Text(fileSize: number) {
  let i = 0;
  for (; i < SIZES_TABLE.length - 1; i++) {
    const element = SIZES_TABLE[i];
    const size = fileSize / element.size;
    if (size < 100) {
      return `${size.toFixed(2)} ${element.name}`;
    }
  }

  const element = SIZES_TABLE[i];
  const size = fileSize / element.size;
  return `${size.toFixed(2)} ${element.name}`;
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
</script>

<style scoped src="./FilesStorage.scss" />
