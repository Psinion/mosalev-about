<template>
  <article class="articles-list-tab">
    <ArticlesList :project-id="-1" />
  </article>
</template>

<script setup lang="ts">
import ArticlesList from "@/modules/projects/shared/ArticlesList/ArticlesList.vue";
</script>

<style scoped src="./ArticlesListTab.scss" />
