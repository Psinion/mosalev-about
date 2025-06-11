<template>
  <article class="articles-list">
    <PermissionChecker>
      <div class="actions">
        <PsiButton
          tag="RouterLink"
          class="action-button"
          :to="articleCreateRoute"
        >
          {{ t('projects.view.articleCreate') }}
        </PsiButton>
      </div>
    </PermissionChecker>
    <div class="articles">
      <template v-if="loading">
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
        <ArticleCardSkeleton />
      </template>
      <template v-else-if="articlesList?.items && articlesList.items.length">
        <ArticleCard
          v-for="article in articlesList.items"
          :key="article.id"
          :article="article"
          @delete="onArticleDeleteClick"
          @change-visibility="onArticleChangeVisibilityClick"
        />

        <PsiPagination
          :current-page="currentPage"
          :limit="limit"
          :total="paginationTotal"
          class="pagination"
          @select-page="onPageSelect"
        />
      </template>
      <div
        v-else
        class="empty-articles-placeholder caption-regular"
      >
        {{ t('projects.view.emptyArticlesPlaceholder') }}
      </div>
    </div>

    <ArticleDeleteDialog
      v-model:visible="articleDeleteDialogVisible"
      :article-id="articleDeleteDialogValue?.id"
      @delete="onArticleDelete"
    >
    </ArticleDeleteDialog>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { computed, onMounted, ref, toRef, watch } from "vue";
import { IArticleCompact, IArticlesPagination, TRoute } from "@/shared/types";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { ServerError } from "@/shared/utils/requests/errors.ts";
import ArticlesServiceInstance from "@/shared/services/ArticlesService.ts";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import ArticleCardSkeleton from "@/modules/projects/shared/ArticleCardSkeleton/ArticleCardSkeleton.vue";
import ArticleCard from "@/modules/projects/shared/ArticleCard/ArticleCard.vue";
import ArticleDeleteDialog from "@/modules/projects/shared/ArticleDeleteDialog/ArticleDeleteDialog.vue";
import { RouteNames } from "@/router/routeNames.ts";
import PsiPagination from "@/shared/PsiUI/components/PsiPagination/PsiPagination.vue";

const props = defineProps({
  projectId: {
    type: Number,
    default: null
  },
  currentPage: {
    type: Number,
    default: 1
  }
});

const emit = defineEmits({
  "update:currentPage": (page: number) => true
});

const toaster = useToaster();
const { t } = useI18n();
const userStore = useUserStore();
const articlesService = ArticlesServiceInstance;

const limit = 5;
const currentPage = toRef(props, "currentPage");
const paginationTotal = ref(0);

const loading = ref(false);
const articlesList = ref<IArticlesPagination | null>(null);

const articleDeleteDialogVisible = ref(false);
const articleDeleteDialogValue = ref<IArticleCompact | null>(null);

const articleCreateRoute = computed<TRoute>(() => {
  return {
    name: RouteNames.ArticleEdit,
    query: {
      project: props.projectId
    }
  };
});

onMounted(async () => {
  await refreshArticles();
});

async function refreshArticles(offset?: number) {
  try {
    loading.value = true;

    if (props.projectId) {
      articlesList.value = await articlesService.getCompactArticlesByProject({
        projectId: props.projectId,
        limit: limit,
        offset: offset
      });
      paginationTotal.value = articlesList.value.totalCount;
    }
    else {
      articlesList.value = await articlesService.getCompactArticles({
        limit: limit,
        offset: offset
      });
      paginationTotal.value = articlesList.value.totalCount;
    }

    loading.value = false;
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}

watch(() => userStore.locale, () => refreshArticles());

const onPageSelect = async (currentPage: number = 1, offset?: number) => {
  emit("update:currentPage", currentPage);
  await refreshArticles(offset);
};

function onArticleDeleteClick(article: IArticleCompact) {
  articleDeleteDialogValue.value = article;
  articleDeleteDialogVisible.value = true;
}

async function onArticleChangeVisibilityClick(article: IArticleCompact, visible: boolean) {
  try {
    await articlesService.changeArticleVisibility(article.id, { visible: visible });
    article.visible = visible;
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}

function onArticleDelete(articleId: number) {
  const foundIndex = articlesList.value.items.findIndex(x => x.id === articleId);
  if (foundIndex !== -1) {
    articlesList.value.items.splice(foundIndex, 1);
  }
}
</script>

<style scoped src="./ArticlesList.scss" />
