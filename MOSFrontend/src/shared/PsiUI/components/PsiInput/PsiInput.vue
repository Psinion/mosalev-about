<template>
  <section
    class="psi-input"
    :class="{'disabled': disabled}"
  >
    <label>
      <span class="caption-regular">{{ label }}</span>
      <input
        :value="inputValue"
        :type="type"
        @input="onInput($event.target as HTMLInputElement)"
      >
      {{ errorMessage }}
    </label>
  </section>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { type TPsiInputType } from "./types.ts";
import { GenericValidateFunction, useField } from "vee-validate";
import { useComponentId } from "@/shared/PsiUI/utils/componentId.ts";
import useValidationRules from "@/shared/PsiUI/utils/validationRules.ts";

const props = defineProps({
  modelValue: {
    type: String as PropType<string | null>,
    default: null
  },
  type: {
    type: String as PropType<TPsiInputType>,
    default: "text"
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
  "update:modelValue": (value: string | null) => true
});

const componentId = useComponentId("PsiInput");

const validateRules = computed(() => {
  const validateFunctions: GenericValidateFunction<string | null>[] = [];

  const rules = useValidationRules();

  if (props.required) {
    validateFunctions.push(rules.required);
  }

  return validateFunctions;
});
const {
  value: inputValue,
  errorMessage
} = useField(componentId, validateRules.value, {
  initialValue: props.modelValue,
  syncVModel: true
});

function onInput(target: HTMLInputElement) {
  emit("update:modelValue", target?.value ? target.value : null);
}

</script>

<style scoped src="./PsiInput.scss" />
