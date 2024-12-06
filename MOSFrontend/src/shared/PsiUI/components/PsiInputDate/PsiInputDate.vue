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

import { computed, PropType, toRef, watch } from "vue";
import { GenericValidateFunction, useField } from "vee-validate";
import { useComponentId } from "@/shared/PsiUI/utils/componentId.ts";
import useValidationRules from "@/shared/PsiUI/utils/validationRules.ts";
import { PsiDictionary } from "@/shared/PsiUI/types/base.ts";

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
  mask: {
    type: String,
    default: "YYYY-MM-DD"
  }
});

const emit = defineEmits({
  "update:modelValue": (value: string | Date | null) => true
});

const componentId = useComponentId("PsiInputDate");

const mask = toRef(props, "mask");

const maskPattern = computed(() => getRegExpPattern(mask.value));

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
  handleChange,
  errorMessage
} = useField(componentId, validateRules.value, {
  initialValue: props.modelValue ? formatDate(props.modelValue, mask.value) : null
});

watch(() => props.modelValue, value => handleChange(value ? formatDate(value, mask.value) : null));

function formatDate(date: string | Date, format: string) {
  let formattedDate = format;

  let dateToFormat: Date | null;
  if (!(date instanceof Date)) {
    dateToFormat = createDate(date);
    if (dateToFormat === null) {
      return date;
    }
  }
  else {
    dateToFormat = date;
  }

  const templates = {
    "M+": dateToFormat.getMonth() + 1,
    "D+": dateToFormat.getDate(),
    "H+": dateToFormat.getHours(),
    "m+": dateToFormat.getMinutes(),
    "s+": dateToFormat.getSeconds()
  };

  const yearMatches = formattedDate.match(/(Y+)/);
  if (yearMatches) {
    const match = yearMatches[0];
    const year = dateToFormat.getFullYear();

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

function checkDate(value: string | Date | null) {
  return value === null || value instanceof Date;
}

function getDateMatches(value: string) {
  const dateRegExp = new RegExp(maskPattern.value.pattern);
  return value.match(dateRegExp);
}

function isValidDate(value: string | Date | null) {
  if (checkDate(value) || getDateMatches(value)) {
    return true;
  }

  return "Не корректная дата";
}

function getRegExpPattern(mask: string) {
  const templates = [
    {
      mask: "YYYY",
      regExp: "([0-9]{4})"
    },
    {
      mask: "MM",
      regExp: "([0-9]{2})"
    },
    {
      mask: "DD",
      regExp: "([0-9]{2})"
    }
  ];

  let pattern = mask;
  let indexMap: PsiDictionary<number> = {};
  for (let i = 0; i < templates.length; i++) {
    const template = templates[i];
    pattern = pattern.replace(template.mask, template.regExp);
    indexMap[template.mask] = i;
  }

  return {
    pattern,
    indexMap
  };
}

function createDate(dateString: string) {
  const matches = getDateMatches(dateString);
  if (matches) {
    const indexMap = maskPattern.value.indexMap;
    const date = new Date();

    if (indexMap["YYYY"] != undefined) {
      const year = parseInt(matches[indexMap["YYYY"] + 1]);
      date.setFullYear(year);
    }
    if (indexMap["MM"] != undefined) {
      const month = parseInt(matches[indexMap["MM"] + 1]);
      date.setMonth(month - 1);
    }
    if (indexMap["DD"] != undefined) {
      const day = parseInt(matches[indexMap["DD"] + 1]);
      date.setDate(day);
    }

    return date;
  }

  return null;
}

function onInput(target: HTMLInputElement) {
  const value = target?.value.replace(/[^0-9-]/g, "");
  const date = createDate(value);
  if (date) {
    const dateText = `${date.getFullYear()}-${date.getMonth() + 1}-${date.getDate()}`;
    emit("update:modelValue", dateText);
  }
  else {
    emit("update:modelValue", value);
  }
}

</script>

<style scoped src="./PsiInputDate.scss" />
