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
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";
import { TPsiIcon } from "@/shared/PsiUI/components/PsiIcon/types.ts";
import { TPsiToasterTheme } from "@/shared/PsiUI/components/PsiToast/types.ts";

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
    type: String as PropType<TPsiIcon>,
    default: null
  },
  theme: {
    type: String as PropType<TPsiToasterTheme>,
    default: "neutral"
  }
});
</script>

<style lang="scss" src="./PsiToast.scss" scoped />
