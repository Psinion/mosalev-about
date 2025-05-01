<template>
  <section
    class="psi-textarea"
    :class="{'disabled': disabled}"
  >
    <label>
      <span class="caption-regular">{{ label }}</span>
      <textarea
        class="caption-regular"
        :style="{resize: resizable}"
        :value="inputValue"
        :rows="rows"
        autocomplete="off"
        @focus="onFocus"
        @blur="onBlur"
        @input="onInput($event.target as HTMLInputElement)"
      />
    </label>
    <div
      v-if="errorMessage"
      class="error-text hint-regular"
    >
      {{ errorMessage }}
    </div>
  </section>
</template>

<script setup lang="ts">

import { computed, inject, onMounted, PropType, watch } from "vue";
import useValidationRules from "@/shared/PsiUI/utils/validationRules.ts";
import { TPsiTextareaResizable } from "@/shared/PsiUI/components/PsiTextarea/types.ts";
import {
  PsiValidateFunction,
  RegisterValidatorFunction,
  usePsiValidation
} from "@/shared/PsiUI/validate/psiValidate.ts";

const props = defineProps({
  modelValue: {
    type: String as PropType<string | undefined>,
    default: undefined
  },
  label: {
    type: String,
    default: null
  },
  required: {
    type: Boolean,
    default: false
  },
  resizable: {
    type: String as PropType<TPsiTextareaResizable>,
    default: "vertical"
  },
  rows: {
    type: Number,
    default: 3
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

watch(
  () => props.modelValue,
  () => {
    inputValue.value = props.modelValue;
  }
);

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

<style scoped src="./PsiTextarea.scss" />
