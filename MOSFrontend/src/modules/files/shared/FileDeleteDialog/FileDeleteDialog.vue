<template>
  <PsiDialog
    :visible="visible"
    hide-when-click-outside
    @update:visible="$emit('update:visible', $event)"
    @confirm="save"
  >
    <template #header>
      {{ t('files.dialogDelete.title') }}
    </template>

    <div class="project-delete-dialog">
      {{ t('files.dialogDelete.description', { fileName: file.originalName }) }}
    </div>
  </PsiDialog>
</template>

<script setup lang="ts">
import PsiDialog from "@/shared/PsiUI/components/PsiDialog/PsiDialog.vue";
import { useI18n } from "vue-i18n";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { PropType, toRef } from "vue";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import FilesServiceInstance from "@/shared/services/FilesService.ts";
import { IUploadedFile } from "@/shared/types";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  file: {
    type: [Object, null] as PropType<IUploadedFile | null>,
    default: null
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "delete": (value: number) => true
});

const toaster = useToaster();
const { t } = useI18n();
const filesService = FilesServiceInstance;

const file = toRef(props, "file");

const save = async () => {
  if (!file.value) {
    return;
  }

  try {
    await filesService.deleteFile(file.value.id);
    emit("delete", file.value.id);
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
};
</script>

<style scoped src="./FileDeleteDialog.scss" />
