<template>
  <section class="psi-toggle">
    <label class="toggle">
      <input
        type="checkbox"
        :checked="modelValue"
        @change="onChange(($event.target as HTMLInputElement).checked)"
      >
      <span class="toggle-switch" />
    </label>
    <span
      v-if="label"
      class="label font-color caption-regular"
      :class="{'active': props.modelValue, 'inactive': !props.modelValue}"
    >
      {{ label }}
    </span>
  </section>
</template>

<script setup lang="ts">
import { computed, PropType } from "vue";

const props = defineProps({
  modelValue: {
    type: Boolean,
    default: null
  },
  /**
   * Название переключателя в активном состоянии.
   * Отображается всегда, если inactiveLabel пустой.
   */
  activeLabel: {
    type: String as PropType<string | null>,
    default: null
  },
  /**
   * Название переключателя в не активном состоянии.
   * Можно оставить пустым, чтобы всегда отображалось activeLabel.
   */
  inactiveLabel: {
    type: String as PropType<string | null>,
    default: null
  }
});

const emit = defineEmits({
  "update:modelValue": (value: boolean) => true
});

const label = computed<string | undefined>(() => {
  const aLabel = props.activeLabel;
  const iLabel = props.inactiveLabel;
  if (aLabel == null) {
    return undefined;
  }

  if (iLabel == null) {
    return aLabel;
  }

  return props.modelValue ? aLabel : iLabel;
});

function onChange(checked: boolean) {
  emit("update:modelValue", checked);
}
</script>

<style scoped src="./PsiToggle.scss" />
