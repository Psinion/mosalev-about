<template>
  <div class="project-view-skeleton">
    <PsiSkeleton
      class="header"
      height="32px"
      width="200px"
    />

    <div class="content">
      <PsiSkeleton
        height="20px"
        width="100%"
      />
      <PsiSkeleton
        height="20px"
        width="100%"
      />
      <PsiSkeleton
        height="20px"
        width="100%"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import PsiSkeleton from "@/shared/PsiUI/components/PsiSkeleton/PsiSkeleton.vue";
</script>

<style scoped src="./ProjectViewSkeleton.scss" />
