<template>
  <section
    class="psi-input-numeric"
    :class="{'disabled': disabled}"
  >
    <label>
      <span class="caption-regular">{{ label }}</span>
      <input
        :value="inputValue"
        type="number"
        step="any"
        :min="min"
        :max="max"
        @focus="onFocus"
        @blur="onBlur"
        @input="onInput($event.target as HTMLInputElement)"
      >
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
import {
  PsiValidateFunction,
  RegisterValidatorFunction,
  usePsiValidation
} from "@/shared/PsiUI/validate/psiValidate.ts";

const props = defineProps({
  modelValue: {
    type: [Number, String] as PropType<number | string | undefined>,
    default: undefined
  },
  min: {
    type: Number as PropType<number>,
    default: Number.MIN_SAFE_INTEGER
  },
  max: {
    type: Number,
    default: Number.MAX_SAFE_INTEGER
  },
  label: {
    type: String,
    default: null
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
  "update:modelValue": (value: number | undefined) => true,
  "focus": () => true,
  "blur": () => true
});

const validateRules = computed(() => {
  const validateFunctions: PsiValidateFunction<string | number | undefined>[] = [];

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
  emit("update:modelValue", target?.value ? Number(target.value) : undefined);
}

</script>

<style scoped src="./PsiInputNumeric.scss" />
