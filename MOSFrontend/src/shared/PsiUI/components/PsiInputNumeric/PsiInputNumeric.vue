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

import { computed, PropType } from "vue";
import { useComponentId } from "@/shared/PsiUI/utils/componentId.ts";
import { GenericValidateFunction, useField } from "vee-validate";
import useValidationRules from "@/shared/PsiUI/utils/validationRules.ts";

const props = defineProps({
  modelValue: {
    type: [Number, String] as PropType<number | string | null>,
    default: null
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
  "update:modelValue": (value: number | null) => true
});

const componentId = useComponentId("PsiInputNumeric");

const validateRules = computed(() => {
  const validateFunctions: GenericValidateFunction<string | number | null>[] = [];

  const rules = useValidationRules();

  if (props.required) {
    validateFunctions.push(rules.required);
  }

  return validateFunctions;
});
const {
  value: inputValue,
  errorMessage
} = useField<string | number | null>(componentId, validateRules.value, {
  initialValue: props.modelValue,
  syncVModel: true
});

function onInput(target: HTMLInputElement) {
  emit("update:modelValue", target?.value ? Number(target.value) : null);
}

</script>

<style scoped src="./PsiInputNumeric.scss" />
