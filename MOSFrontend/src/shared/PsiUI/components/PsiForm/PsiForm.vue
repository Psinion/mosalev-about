<template>
  <form
    class="psi-form"
    @submit.prevent="onSubmit"
  >
    <slot :valid="valid" />
  </form>
</template>

<script setup lang="ts">
import { useForm, useIsFormValid } from "vee-validate";
import { onMounted, onUnmounted, ref, watch } from "vue";

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

const form = useForm();
const isValid = useIsFormValid();

const valid = ref(true);

let autoTimer: ReturnType<typeof setTimeout> | null = null;
watch(() => props.autoSubmitTimerStop, (value) => {
  console.log("autoSubmitTimerStop", value);
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

  if (!props.autoSubmitTimerStop && valid.value) {
    emit("submit");
    emit("update:autoSubmitTimerStop", false);
  }
});

async function onSubmit() {
  await form.validate();
  if (!isValid.value) {
    return;
  }

  emit("submit");
}

async function validate() {
  await form.validate();
  return isValid.value;
}

watch(() => form.meta.value.valid, (value) => {
  valid.value = value;
  emit("valid", value);
});

function setAutoSubmitTimer() {
  if (props.autoSubmitTimer) {
    console.log("setAutoSubmitTimer");
    autoTimer = setTimeout(() => {
      if (!props.autoSubmitTimerStop && valid.value) {
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
