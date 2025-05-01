<template>
  <section
    class="article-card-skeleton"
  >
    <header class="header">
      <PsiSkeleton
        height="24px"
        width="40%"
      />
    </header>
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
    <footer>
      <PsiSkeleton
        height="14px"
        width="20%"
      />
    </footer>
  </section>
</template>

<script setup lang="ts">
import PsiSkeleton from "@/shared/PsiUI/components/PsiSkeleton/PsiSkeleton.vue";
</script>

<style scoped src="./ArticleCardSkeleton.scss" />
