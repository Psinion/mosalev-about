<template>
  <section
    class="psi-slider"
    :class="{'disabled': disabled}"
  >
    <label>
      <span class="caption-regular">{{ label }}</span>
      <input
        :value="inputValue"
        type="range"
        :min="min"
        :max="max"
        :step="step"
        @focus="onFocus"
        @blur="onBlur"
        @input="onInput($event.target as HTMLInputElement)"
      >
    </label>
  </section>
</template>

<script setup lang="ts">

import { ref, watch } from "vue";

const props = defineProps({
  modelValue: {
    type: Number,
    default: 0
  },
  label: {
    type: String,
    default: null
  },
  min: {
    type: Number,
    default: 0
  },
  max: {
    type: Number,
    default: 10
  },
  step: {
    type: Number,
    default: 1
  },
  disabled: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:modelValue": (value: string | undefined) => true,
  "focus": () => true,
  "blur": () => true
});

const inputValue = ref(props.min);

watch(
  () => props.modelValue,
  () => {
    inputValue.value = props.modelValue;
  }
);

function onFocus() {
  emit("focus");
}

function onBlur() {
  emit("blur");
}

function onInput(target: HTMLInputElement) {
  emit("update:modelValue", target?.value ? target.value : undefined);
}

</script>

<style scoped src="./PsiSlider.scss" />
