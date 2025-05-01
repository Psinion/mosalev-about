<template>
  <section class="project-card-skeleton">
    <PsiSkeleton
      height="24px"
      width="100%"
    />
    <div class="description">
      <PsiSkeleton
        height="14px"
        width="80%"
      />
      <PsiSkeleton
        height="14px"
        width="50%"
      />
      <PsiSkeleton
        height="14px"
        width="90%"
      />
    </div>
  </section>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import PsiSkeleton from "@/shared/PsiUI/components/PsiSkeleton/PsiSkeleton.vue";

const { t } = useI18n();

</script>

<style scoped src="./ProjectCardSkeleton.scss" />
