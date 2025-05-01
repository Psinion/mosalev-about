<template>
  <ContentLayout>
    <div class="project-view">
      <ProjectViewSkeleton v-if="loadFirst" />
      <template v-else>
        <div class="content">
          <h2>{{ currentProject.title }}</h2>

          <div class="actions">
            <PermissionChecker>
              <PsiButton
                tag="RouterLink"
                class="action-button"
                :to="articleCreateRoute"
              >
                {{ t('projects.view.articleCreate') }}
              </PsiButton>
            </PermissionChecker>
          </div>

          <div class="description caption-regular">
            {{ currentProject.description }}
          </div>
        </div>
        <div class="articles">
          <template v-if="loadData">
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
          </template>
          <div
            v-else
            class="empty-articles-placeholder caption-regular"
          >
            {{ t('projects.view.emptyArticlesPlaceholder') }}
          </div>
        </div>
      </template>

      <ArticleDeleteDialog
        v-model:visible="articleDeleteDialogVisible"
        :article-id="articleDeleteDialogValue?.id"
        @delete="onArticleDelete"
      >
      </ArticleDeleteDialog>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">

import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { computed, onMounted, ref } from "vue";
import { IArticleCompact, IArticlesPagination, IProject, IProjectCompact, TRoute } from "@/shared/types";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectViewSkeleton from "@/modules/projects/pages/ProjectView/ProjectViewSkeleton/ProjectViewSkeleton.vue";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import ArticlesServiceInstance from "@/shared/services/ArticlesService.ts";
import ArticleCard from "@/modules/projects/shared/ArticleCard/ArticleCard.vue";
import ArticleCardSkeleton from "@/modules/projects/shared/ArticleCardSkeleton/ArticleCardSkeleton.vue";
import ArticleDeleteDialog from "@/modules/projects/shared/ArticleDeleteDialog/ArticleDeleteDialog.vue";

const props = defineProps({
  projectId: {
    type: Number,
    default: null
  }
});

const router = useRouter();
const toaster = useToaster();
const { t } = useI18n();
const projectsService = ProjectsServiceInstance;
const articlesService = ArticlesServiceInstance;

const loadFirst = ref(true);
const loadData = ref(true);
const currentProject = ref<IProject>();
const articlesList = ref<IArticlesPagination>();

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
  try {
    loadFirst.value = true;
    currentProject.value = await projectsService.getProject(props.projectId);
    loadFirst.value = false;
  }
  catch (error) {
    if (error instanceof ServerError) {
      if (error.statusCode === 404) {
        await router.push({ name: RouteNames.Error404 });
        return;
      }
      toaster.error(error.header, error.message);
    }
  }

  await refreshArticles();
});

async function refreshArticles() {
  try {
    loadData.value = true;
    articlesList.value = await articlesService.getCompactArticles({
      projectId: props.projectId
    });

    loadData.value = false;
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}

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

<style scoped src="./ProjectView.scss" />
