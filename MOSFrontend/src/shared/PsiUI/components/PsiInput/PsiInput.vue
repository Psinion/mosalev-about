<template>
  <section
    class="psi-input"
    :class="{'disabled': disabled}"
  >
    <label>
      <span class="caption-regular">{{ label }}</span>
      <input
        :value="inputValue"
        :type="password ? 'password' : 'text'"
        autocomplete="off"
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
  NotifyValidityFunction,
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
  disabled: {
    type: Boolean,
    default: false
  },
  password: {
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

<style scoped src="./PsiInput.scss" />
