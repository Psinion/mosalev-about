<template>
  <div
    class="psi-file-upload-area"
    :class="diFileUploadAreaClasses"
    @dragover.prevent="dragOver"
    @drop.prevent="drop"
    @click="onAreaClick"
  >
    <PsiTransitionFade
      :visible="!disabled && isDragging"
      leave-speed="instant"
    >
      <div class="drag-placeholder caption-regular">
        <PsiIcon icon="file" />
        Перетащите файлы сюда
      </div>
    </PsiTransitionFade>

    <div v-show="!(!disabled && isDragging)">
      <input
        ref="fileUploadInputRef"
        type="file"
        hidden
        multiple
        @change="onFilesSelect"
      />
      <div class="content">
        <template v-if="area">
          <slot />
        </template>
        <PsiButton
          v-else
          icon="file"
          theme="primary"
          :disabled="disabled"
          @click="onAddFilesButtonClick"
        >
          {{
            buttonTitle
          }}
        </PsiButton>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { computed, onMounted, onUnmounted, ref } from "vue";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiTransitionFade from "@/shared/PsiUI/components/PsiTransitionFade/PsiTransitionFade.vue";

const props = defineProps({
  buttonTitle: {
    type: String,
    default: "Выбрать файлы"
  },
  fileSizeMax: {
    type: Number,
    default: 1024 * 1024
  },
  filePossibleTypes: {
    type: String,
    default: null
  },
  disabled: {
    type: Boolean,
    default: false
  },
  area: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  filesUpload: (value: FileList) => true,
  fileTooLarge: () => true,
  incorrectFileType: () => true
});

const fileUploadInputRef = ref();

const lastTarget = ref<EventTarget | null>(null);
const isDragging = ref(false);

const diFileUploadAreaClasses = computed(() => [
  { dragging: isDragging.value },
  { disabled: props.disabled },
  { area: props.area }
]);

onMounted(() => {
  window.addEventListener("dragenter", dragStart);
  window.addEventListener("dragleave", dragEnd);
});

onUnmounted(() => {
  window.removeEventListener("dragenter", dragStart);
  window.removeEventListener("dragleave", dragEnd);
});

function onAddFilesButtonClick() {
  fileUploadInputRef.value.click();
}

function onAreaClick() {
  if (props.area) {
    onAddFilesButtonClick();
  }
}

function dragStart(event: Event) {
  event.preventDefault();
  lastTarget.value = event.target;
  isDragging.value = true;
}

function dragEnd(event: Event) {
  event.preventDefault();
  if (event.target === lastTarget.value) {
    isDragging.value = false;
  }
}

function dragOver() {
  isDragging.value = true;
}

function drop(event: DragEvent) {
  isDragging.value = false;
  if (event.dataTransfer?.files) {
    for (let i = 0; i < event.dataTransfer.files.length; i++) {
      const file = event.dataTransfer.files[i];
      if (!validateFile(file)) {
        return;
      }
    }

    emit("filesUpload", event.dataTransfer.files);
  }
}

function onFilesSelect() {
  const file = fileUploadInputRef.value.files[0] as File;
  if (validateFile(file)) {
    emit("filesUpload", fileUploadInputRef.value.files);
  }
}

function validateFile(file: File) {
  if (file.size > props.fileSizeMax) {
    emit("fileTooLarge");
    return false;
  }

  if (props.filePossibleTypes) {
    const fileExtension = file.name.split(".").pop()?.toLowerCase();
    if (fileExtension) {
      const foundType = props.filePossibleTypes.indexOf(fileExtension);
      if (foundType === -1) {
        emit("incorrectFileType");
        return false;
      }
    }
  }

  return true;
}
</script>

<style lang="scss" scoped src="./PsiFileUpload.scss"></style>
