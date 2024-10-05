<template>
  <section class="footer-link">
    <a :href="href" target="_blank">
      <PsiIcon :name="icon" :scale="1" />
      <span class="footer-link-title body-regular">{{title}}</span>
    </a>
  </section>
</template>

<script setup lang="ts">

import { PropType } from 'vue';
import PsiIcon from "@/shared/components/PsiIcon/PsiIcon.vue";
import { TIcon } from '@/shared/components/PsiIcon/types.ts';

defineProps({
  icon: {
    type: String as PropType<TIcon>,
    required: true
  },
  href: {
    type: String,
    required: true
  },
  title: {
    type: String,
    default: null,
  },
});

</script>

<style scoped src="./FooterLink.scss" />
