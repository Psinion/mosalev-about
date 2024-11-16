<template>
  <section
    class="psi-input"
    :class="{'disabled': disabled}"
  >
    <label>
      <span class="caption-regular">{{ label }}</span>
      <input
        :value="modelValue"
        :type="type"
        @input="onInput($event.target as HTMLInputElement)"
      >
    </label>
  </section>
</template>

<script setup lang="ts">

import { PropType } from "vue";
import { TPsiInputType } from "@/shared/components/PsiInput/types.ts";

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
  disabled: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:modelValue": (value: string | null) => true
});

function onInput(target: HTMLInputElement) {
  emit("update:modelValue", target?.value ? target.value : null);
}

</script>

<style scoped src="./PsiInput.scss" />
