<template>
  <section class="footer-link">
    <a
      :href="href"
      target="_blank"
    >
      <PsiIcon
        :icon="icon"
        :scale="1"
      />
      <span class="footer-link-title body-regular">{{ title }}</span>
    </a>
  </section>
</template>

<script setup lang="ts">

import { PropType } from "vue";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";
import { TPsiIcon } from "@/shared/PsiUI/components/PsiIcon/types.ts";

defineProps({
  icon: {
    type: String as PropType<TPsiIcon>,
    required: true
  },
  href: {
    type: String,
    required: true
  },
  title: {
    type: String,
    default: null
  }
});

</script>

<style scoped src="./FooterLink.scss" />
