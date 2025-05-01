<template>
  <RouterLink
    :to="articleViewRoute"
    class="article-card"
  >
    <header class="header">
      <h3>{{ article.title }}</h3>

      <div
        class="actions"
      >
        <PermissionChecker>
          <PsiButton
            flat
            :icon="article.visible ? 'eye' : 'eye-crossed'"
            @click.prevent="$emit('changeVisibility', article, !article.visible)"
          />
          <PsiButton
            flat
            icon="edit"
            @click.prevent="$emit('edit', article)"
          />
          <PsiButton
            flat
            icon="trash-box"
            @click.prevent="$emit('delete', article)"
          />
        </PermissionChecker>
      </div>
    </header>
    <div class="description caption-regular">
      {{ article.description }}
    </div>
    <footer>
      <div class="hint-regular tertiary">
        {{ dateUpdate }}
      </div>
    </footer>
  </RouterLink>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { IArticleCompact } from "@/shared/types";
import { formatDate } from "@/shared/utils/dateHelpers.ts";
import { RouteNames } from "@/router/routeNames.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";

const props = defineProps({
  article: {
    type: Object as PropType<IArticleCompact>,
    required: true
  }
});

const emit = defineEmits({
  changeVisibility: (project: IArticleCompact, visibility: boolean) => true,
  edit: (value: IArticleCompact) => true,
  delete: (value: IArticleCompact) => true
});

const dateUpdate = computed(() => {
  const date = props.article.updatedAt ?? props.article.createdAt;
  return `${formatDate(date, "YYYY.MM.DD HH:mm")}`;
});

const articleViewRoute = computed(() => {
  return {
    name: RouteNames.ProjectView,
    params: { projectId: props.article?.id }
  };
});
</script>

<style scoped src="./ArticleCard.scss" />
