<template>
  <section
    class="file-card"
    @click="copyUrlToClipboard"
  >
    <header class="header">
      <h4
        v-tooltip="{
          text: file.originalName,
          width: '220px',
        }"
      >
        {{ file.originalName }}
      </h4>

      <div class="actions">
        <PermissionChecker>
          <PsiButton
            flat
            icon="trash-box"
            @click.prevent.stop="$emit('delete', file)"
          />
        </PermissionChecker>
      </div>
    </header>

    <footer class="footer">
      <div class="hint-regular tertiary-up">
        {{ fileSize }}
      </div>
      <div class="hint-regular tertiary">
        {{ dateUpdate }}
      </div>
    </footer>
  </section>
</template>

<script setup lang="ts">
import { computed, PropType } from "vue";
import { IUploadedFile } from "@/shared/types";
import { formatDate } from "@/shared/utils/dateHelpers.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import { fileSize2Text } from "@/modules/files/utils/files.ts";

const props = defineProps({
  file: {
    type: Object as PropType<IUploadedFile>,
    required: true
  }
});

const emit = defineEmits({
  delete: (value: IUploadedFile) => true
});

const toaster = useToaster();
const { t } = useI18n();

const fileSize = computed(() => fileSize2Text(props.file.size));

const dateUpdate = computed(() => {
  const date = props.file.uploadDate;
  return `${formatDate(date, "YYYY.MM.DD HH:mm")}`;
});

function copyUrlToClipboard() {
  navigator.clipboard.writeText(props.file.url);
  toaster.neutral(t("forms.copyToClipboard"));
}
</script>

<style scoped src="./FileCard.scss" />
