<template>
  <section
    class="psi-markdown-editor"
    :class="{'disabled': disabled}"
  >
    <div
      class="editor"
      :style="{height: height}"
    >
      <textarea
        :value="inputValue"
        class="caption-regular"
        autocomplete="off"
        @focus="onFocus"
        @blur="onBlur"
        @input="onInput($event.target as HTMLInputElement)"
      />

      <div
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
import { computed, inject, onMounted, PropType, ref, watch } from "vue";
import { markedInstance } from "@/setup/marked.ts";
import {
  PsiValidateFunction,
  RegisterValidatorFunction,
  usePsiValidation
} from "@/shared/PsiUI/validate/psiValidate.ts";
import useValidationRules from "@/shared/PsiUI/utils/validationRules.ts";

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
