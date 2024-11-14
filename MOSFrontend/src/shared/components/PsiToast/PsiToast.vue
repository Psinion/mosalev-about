<template>
  <section
    class="psi-toast"
    :class="theme"
  >
    <PsiIcon
      v-if="icon"
      :icon="icon"
    />
    <div class="content">
      <span class="body-regular font-color">{{ header }}</span>
      <span class="hint-regular tertiary-up">{{ description }}</span>
    </div>
  </section>
</template>

<script setup lang="ts">
import type { PropType } from "vue";
import type { TPsiToasterTheme } from "@/shared/components/PsiToast/types";
import PsiIcon from "@/shared/components/PsiIcon/PsiIcon.vue";
import { TIcon } from "@/shared/components/PsiIcon/types.ts";

const props = defineProps({
  header: {
    type: String,
    default: null
  },
  description: {
    type: String,
    default: null
  },
  icon: {
    type: String as PropType<TIcon>,
    default: null
  },
  theme: {
    type: String as PropType<TPsiToasterTheme>,
    default: "neutral"
  }
});
</script>

<style lang="scss" src="./PsiToast.scss" scoped />
