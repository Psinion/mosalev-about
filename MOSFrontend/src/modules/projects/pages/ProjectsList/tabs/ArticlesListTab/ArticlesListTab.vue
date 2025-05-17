<template>
  <article class="articles-list-tab">
    <ArticlesList
      :current-page="projectsArticlesTabQuery.page"
      @update:current-page="onPageChange"
    />
  </article>
</template>

<script setup lang="ts">
import ArticlesList from "@/modules/projects/shared/ArticlesList/ArticlesList.vue";
import { computed } from "vue";
import { useProjectsListStore } from "@/modules/projects/stores/projectsListStore.ts";

const projectsListStore = useProjectsListStore();

const projectsArticlesTabQuery = computed(() => projectsListStore.projectsArticlesTabQuery);

const onPageChange = (page: number) => {
  projectsArticlesTabQuery.value.page = page;
  projectsListStore.updateQueryToUrl();
};
</script>

<style scoped src="./ArticlesListTab.scss" />
