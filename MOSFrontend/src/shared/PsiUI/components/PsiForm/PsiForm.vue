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
import { PsiResetter, PsiValidator } from "@/shared/PsiUI/validate/psiValidate.ts";

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

const validateRules = ref(new Set<PsiValidator>());
const validateStates = ref(new Map<PsiValidator, boolean>());
const resetters = ref(new Set<PsiResetter>());
const isValid = computed(() => {
  console.log(validateStates.value);
  const valid = Array.from(validateStates.value.values()).every(v => v === true);
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

function registerValidator(validator: PsiValidator, resetter: PsiResetter) {
  validateRules.value.add(validator);
  validateStates.value.set(validator, true);
  resetters.value.add(resetter);
}

function notifyValidity(validator: PsiValidator, isValid: boolean) {
  validateStates.value.set(validator, isValid);
}

provide("registerValidator", registerValidator);
provide("notifyValidity", notifyValidity);

async function validate(): Promise<boolean> {
  const results = await Promise.all(
    [...validateRules.value].map(validate => validate())
  );
  return results.every(valid => valid);
}

/**
 * Reset touched and invalid states
 */
function reset() {
  const states = validateStates.value;
  states.forEach((value, key) => {
    states.set(key, false);
  });
  resetters.value.forEach(reset => reset());
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
    autoTimer = setTimeout(() => {
      if (!props.autoSubmitTimerStop && isValid.value) {
        onSubmit();
        emit("update:autoSubmitTimerStop", true);
      }
    }, props.autoSubmitTimer);
  }
}

defineExpose({
  isValid,

  validate,
  reset
});
</script>
