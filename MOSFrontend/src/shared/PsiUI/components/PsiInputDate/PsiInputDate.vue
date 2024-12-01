<template>
  <section
    class="psi-input-date"
    :class="{'disabled': disabled}"
  >
    <label>
      <span class="caption-regular">{{ label }}</span>
      <input
        :value="inputValue"
        type="text"
        autocomplete="off"
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

import { computed, PropType, toRef } from "vue";
import { GenericValidateFunction, useField } from "vee-validate";
import { useComponentId } from "@/shared/PsiUI/utils/componentId.ts";
import useValidationRules from "@/shared/PsiUI/utils/validationRules.ts";

const props = defineProps({
  modelValue: {
    type: [String, Date] as PropType<string | Date | null>,
    default: null
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
  format: {
    type: String,
    default: "yyyy-MM-dd"
  }
});

const emit = defineEmits({
  "update:modelValue": (value: string | null) => true
});

const componentId = useComponentId("PsiInputDate");

const format = toRef(props, "format");

const validateRules = computed(() => {
  const validateFunctions: GenericValidateFunction<string | Date | null>[] = [isValidDate];
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
  initialValue: props.modelValue ? formatDate(new Date(props.modelValue), format.value) : null,
  syncVModel: true
});

function formatDate(date: Date, format: string) {
  let formattedDate = format;

  const templates = {
    "M+": date.getMonth() + 1,
    "d+": date.getDate(),
    "h+": date.getHours(),
    "m+": date.getMinutes(),
    "s+": date.getSeconds()
  };

  const yearMatches = formattedDate.match(/(y+)/);
  if (yearMatches) {
    const match = yearMatches[0];
    const year = date.getFullYear();

    formattedDate = formattedDate.replace(match, year.toString());
  }

  for (const template in templates) {
    const regExp = new RegExp(`(${template})`);
    const matches = formattedDate.match(regExp);
    if (matches) {
      const match = matches[0];
      const word = (templates[template] as number).toString();
      formattedDate = formattedDate.replace(
        match,
        word.length !== 1 ? word : `0${word}`
      );
    }
  }

  return formattedDate;
}

function isValidDate(value: string | Date | null) {
  if (value === null || value instanceof Date) {
    return true;
  }

  const dateRegExp = new RegExp("[0-9]{2}.[0-9]{2}.[0-9]{4}");
  if (value.match(dateRegExp)) {
    return true;
  }

  return "Не корректная дата";
}

function onInput(target: HTMLInputElement) {
  emit("update:modelValue", target?.value ? target.value : null);
}

</script>

<style scoped src="./PsiInputDate.scss" />
