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

import { computed, inject, onMounted, PropType, toRef, watch } from "vue";
import useValidationRules from "@/shared/PsiUI/utils/validationRules.ts";
import { PsiDictionary } from "@/shared/PsiUI/types/base.ts";
import {
  PsiValidateFunction,
  RegisterValidatorFunction,
  usePsiValidation
} from "@/shared/PsiUI/validate/psiValidate.ts";

const props = defineProps({
  modelValue: {
    type: [String, Date] as PropType<string | Date | undefined>,
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
  "update:modelValue": (value: string | Date | undefined) => true,
  "focus": () => true,
  "blur": () => true
});

const mask = toRef(props, "mask");

const maskPattern = computed(() => getRegExpPattern(mask.value));

const validateRules = computed(() => {
  const validateFunctions: PsiValidateFunction<string | Date | undefined | null>[] = [isValidDate];
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

watch(() => props.modelValue, value => inputValue.value = value ? formatDate(value, mask.value) : undefined);

const registerValidator = inject<RegisterValidatorFunction>("registerValidator");

onMounted(() => {
  if (registerValidator) {
    registerValidator(validate, reset);
  }
});

function formatDate(date: string | Date, format: string) {
  let formattedDate = format;
  let dateToFormat: Date | undefined;
  if (!(date instanceof Date)) {
    dateToFormat = createDate(date);
    if (dateToFormat === undefined) {
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

  for (const [template, value] of Object.entries(templates)) {
    const regExp = new RegExp(`(${template})`);
    const matches = formattedDate.match(regExp);
    if (matches) {
      const match = matches[0];
      const word = value.toString();
      formattedDate = formattedDate.replace(
        match,
        word.length !== 1 ? word : `0${word}`
      );
    }
  }

  return formattedDate;
}

function getDateMatches(value: string) {
  const dateRegExp = new RegExp(maskPattern.value.pattern);
  return value.match(dateRegExp);
}

function isValidDate(value: string | Date | undefined | null) {
  if (value === undefined || value === null || value instanceof Date || getDateMatches(value)) {
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

  return undefined;
}

function onFocus() {
  emit("focus");
}

function onBlur() {
  handleBlur();
  emit("blur");
}

function onInput(target: HTMLInputElement) {
  const value = target?.value.replace(/[^0-9-]/g, "");
  const date = createDate(value);
  if (date) {
    const month = (date.getMonth() + 1).toString().padStart(2, "0");
    const day = date.getDate().toString().padStart(2, "0");
    const dateText = `${date.getFullYear()}-${month}-${day}`;
    emit("update:modelValue", dateText);
  }
  else {
    emit("update:modelValue", value);
  }
}

</script>

<style scoped src="./PsiInputDate.scss" />
