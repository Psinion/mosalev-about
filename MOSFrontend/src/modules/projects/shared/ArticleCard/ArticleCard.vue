<template>
  <RouterLink
    draggable="false"
    :to="articleViewRoute"
    class="article-card"
    :class="[{hidden: !article.visible}]"
  >
    <header class="header">
      <h3>{{ article.title }}</h3>

      <div class="actions">
        <PermissionChecker>
          <PsiButton
            flat
            :icon="article.visible ? 'eye' : 'eye-crossed'"
            @click.prevent="$emit('changeVisibility', article, !article.visible)"
          />
          <PsiButton
            tag="RouterLink"
            flat
            icon="edit"
            :to="articleEditRoute"
          />
          <PsiButton
            flat
            icon="trash-box"
            @click.prevent="$emit('delete', article)"
          />
        </PermissionChecker>
      </div>
    </header>
    <div
      v-markdown="article.description"
      class="description typography-block caption-regular"
    />
    <footer class="footer">
      <div>
        <RouterLink
          v-if="article.project"
          draggable="false"
          :to="{
            name: RouteNames.ProjectView,
            params: { projectId: article.project.id }
          }"
          class="project-chip hint-regular"
          @click.prevent
        >
          {{ article.project.title }}
        </RouterLink>
      </div>

      <div class="date hint-regular tertiary">
        <PsiIcon icon="calendar" />
        <span>{{ dateCreateString }}</span>
      </div>
    </footer>
  </RouterLink>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { IArticleCompact, TRoute } from "@/shared/types";
import { RouteNames } from "@/router/routeNames.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import { useShortDateCreateUpdate2String } from "@/shared/composables/date.ts";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";

const props = defineProps({
  article: {
    type: Object as PropType<IArticleCompact>,
    required: true
  }
});

const emit = defineEmits({
  changeVisibility: (project: IArticleCompact, visibility: boolean) => true,
  delete: (value: IArticleCompact) => true
});

const articleCreatedAt = computed(() => props.article.createdAt);
const articleUpdatedAt = computed(() => props.article.updatedAt);

const { dateCreateString, dateUpdateString } = useShortDateCreateUpdate2String(articleCreatedAt, articleUpdatedAt);

const articleViewRoute = computed(() => {
  return {
    name: RouteNames.ArticleView,
    params: { articleId: props.article?.id }
  };
});

const articleEditRoute = computed<TRoute>(() => {
  return {
    name: RouteNames.ArticleEdit,
    params: { articleId: props.article?.id }
  };
});
</script>

<style scoped src="./ArticleCard.scss" />
