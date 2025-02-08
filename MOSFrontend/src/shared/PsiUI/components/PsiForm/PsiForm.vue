<template>
  <form
    class="psi-form"
    @submit.prevent="onSubmit"
  >
    <slot :valid="isValid" />
  </form>
</template>

<script setup lang="ts">
import { computed, onMounted, onUnmounted, provide, ref, watch } from "vue";
import { PsiValidator } from "@/shared/PsiUI/validate/psiValidate.ts";

const props = defineProps({
  /**
   * Time to auto submit
   */
  autoSubmitTimer: {
    type: Number,
    default: null
  },
  /**
   * Stop timer to auto submit
   */
  autoSubmitTimerStop: {
    type: Boolean,
    default: true
  }
});

const emit = defineEmits({
  "submit": () => true,
  "valid": (value: boolean) => true,
  "update:autoSubmitTimerStop": (value: boolean) => true
});

const validators = ref(new Set<PsiValidator>());
const validities = ref(new Map<PsiValidator, boolean>());
const isValid = computed(() => {
  const valid = Array.from(validities.value.values()).every(v => v === true);
  emit("valid", valid);
  return valid;
});

let autoTimer: ReturnType<typeof setTimeout> | null = null;
watch(() => props.autoSubmitTimerStop, (value) => {
  if (!value) {
    setAutoSubmitTimer();
  }
});

onMounted(() => {
  setAutoSubmitTimer();
});

onUnmounted(async () => {
  if (autoTimer) {
    clearTimeout(autoTimer);
  }

  if (!props.autoSubmitTimerStop && isValid.value) {
    emit("submit");
    emit("update:autoSubmitTimerStop", false);
  }
});

function registerValidator(validator: PsiValidator) {
  validators.value.add(validator);
  validities.value.set(validator, false);
}

function notifyValidity(validator: PsiValidator, isValid: boolean) {
  validities.value.set(validator, isValid);
}

provide("registerValidator", registerValidator);
provide("notifyValidity", notifyValidity);

async function validate(): Promise<boolean> {
  const results = await Promise.all(
    [...validators.value].map(validate => validate())
  );
  return results.every(valid => valid);
}

async function onSubmit() {
  const valid = validate();
  if (!valid) {
    return;
  }

  emit("submit");
}

function setAutoSubmitTimer() {
  if (props.autoSubmitTimer) {
    console.log("setAutoSubmitTimer");
    autoTimer = setTimeout(() => {
      if (!props.autoSubmitTimerStop && isValid.value) {
        onSubmit();
        emit("update:autoSubmitTimerStop", true);
      }
    }, props.autoSubmitTimer);
  }
}

defineExpose({
  validate
});
</script>
