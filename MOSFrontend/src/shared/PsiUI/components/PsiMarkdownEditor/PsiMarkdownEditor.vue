<template>
  <section
    class="psi-markdown-editor"
    :class="{'disabled': disabled}"
  >
    <div
      v-if="!disabled"
      class="toolbar"
    >
      <div class="left-part">
        <PsiButton
          v-tooltip="'Жирный'"
          @click="insertSyntax('**', '**')"
        >
          B
        </PsiButton>
        <PsiButton
          v-tooltip="'Курсив'"
          @click="insertSyntax('*', '*')"
        >
          I
        </PsiButton>
        <PsiButton
          v-tooltip="'Ссылка'"
          @click="insertSyntax('[', '](https://)')"
        >
          L
        </PsiButton>
      </div>

      <PsiButton
        v-tooltip="hasPreview ? 'Скрыть превью' : 'Показать превью'"
        @click="hasPreview = !hasPreview"
      >
        {{ hasPreview ? 'Hide' : 'Show' }}
      </PsiButton>
    </div>

    <div
      class="editor"
      :style="{height: height}"
    >
      <textarea
        ref="editorFieldRef"
        :value="inputValue"
        class="caption-regular"
        autocomplete="off"
        @focus="onFocus"
        @blur="onBlur"
        @input="onInput($event.target as HTMLInputElement)"
      />

      <div
        v-if="hasPreview"
        class="preview typography-block"
        v-html="compiledMarkdown"
      />
    </div>
    <div
      v-if="errorMessage"
      class="error-text hint-regular"
    >
      {{ errorMessage }}
    </div>
  </section>
</template>

<script setup lang="ts">
import { computed, inject, nextTick, onMounted, PropType, ref, watch } from "vue";
import { markedInstance } from "@/setup/marked.ts";
import {
  PsiValidateFunction,
  RegisterValidatorFunction,
  usePsiValidation
} from "@/shared/PsiUI/validate/psiValidate.ts";
import useValidationRules from "@/shared/PsiUI/utils/validationRules.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";

const props = defineProps({
  modelValue: {
    type: String as PropType<string | undefined>,
    default: undefined
  },
  height: {
    type: String,
    default: "400px"
  },
  required: {
    type: Boolean,
    default: false
  },
  disabled: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:modelValue": (value: string | undefined) => true,
  "focus": () => true,
  "blur": () => true
});

const editorFieldRef = ref<HTMLTextAreaElement | null>(null);
const hasPreview = ref(false);

const validateRules = computed(() => {
  const validateFunctions: PsiValidateFunction<string | undefined>[] = [];

  const rules = useValidationRules();

  if (props.required) {
    validateFunctions.push(rules.required);
  }

  return validateFunctions;
});
const {
  value: inputValue,
  errorMessage,
  validate,
  handleBlur,
  reset
} = usePsiValidation(validateRules.value, {
  initialValue: props.modelValue
});

const compiledMarkdown = ref("");
watch(() => props.modelValue, async (value) => {
  inputValue.value = value;
  compiledMarkdown.value = await markedInstance.parse(value ?? "");
}, {
  immediate: true
});

const registerValidator = inject<RegisterValidatorFunction>("registerValidator");

onMounted(() => {
  if (registerValidator) {
    registerValidator(validate, reset);
  }
});

const insertSyntax = async (before: string, after: string) => {
  const editorField = editorFieldRef.value;
  if (!editorField) {
    return;
  }

  const start = editorField.selectionStart;
  const end = editorField.selectionEnd;
  const originalValue = editorField.value;

  const newValue
    = originalValue.slice(0, start)
    + before + originalValue.slice(start, end) + after
    + originalValue.slice(end);

  emit("update:modelValue", newValue);

  await nextTick();
  const newPos = start + before.length;
  editorField.setSelectionRange(newPos, newPos + (end - start));
  editorField.focus();
};

function onFocus() {
  emit("focus");
}

function onBlur() {
  handleBlur();
  emit("blur");
}

function onInput(target: HTMLInputElement) {
  emit("update:modelValue", target?.value ? target.value : undefined);
}

</script>

<style scoped src="./PsiMarkdownEditor.scss" />
