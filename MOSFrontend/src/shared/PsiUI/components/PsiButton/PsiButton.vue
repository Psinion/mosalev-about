<template>
  <component
    :is="tag"
    class="psi-button caption-regular"
    :class="classes"
    :type="nativeType"
  >
    <PsiIcon
      v-if="icon"
      :icon="icon"
    />
    <slot />
  </component>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { TPsiButtonNativeType, TPsiButtonTag } from "@/shared/PsiUI/components/PsiButton/types.ts";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";
import { TPsiIcon } from "@/shared/PsiUI/components/PsiIcon/types.ts";

const props = defineProps({
  icon: {
    type: String as PropType<TPsiIcon>,
    default: null
  },
  nativeType: {
    type: String as PropType<TPsiButtonNativeType>,
    default: "button"
  },
  disabled: {
    type: Boolean,
    default: false
  },
  flat: {
    type: Boolean,
    default: false
  },
  tag: {
    type: String as PropType<TPsiButtonTag>,
    default: "button"
  }
});

const classes = computed(() => {
  return [{ disabled: props.disabled, flat: props.flat }];
});

</script>

<style scoped src="./PsiButton.scss" />
