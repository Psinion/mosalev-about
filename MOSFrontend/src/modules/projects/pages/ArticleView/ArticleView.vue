<template>
  <ContentLayout>
    <div class="article-view">
      <ProjectViewSkeleton v-if="loading" />
      <template v-else>
        <div class="actions">
          <PsiButton
            v-if="currentArticle?.project"
            tag="RouterLink"
            :to="projectRoute"
            class="action-button"
            icon="left"
          >
            {{ currentArticle?.project.title }}
          </PsiButton>
        </div>

        <div class="content">
          <h2> {{ currentArticle.title }}</h2>

          <div class="actions">
            <PermissionChecker v-if="false">
              <PsiButton
                class="action-button"
              >
                {{ t('forms.save') }}
              </PsiButton>
            </PermissionChecker>
          </div>

          <div
            v-markdown
            class="description typography-block"
          >
            {{ currentArticle.description }}
          </div>
        </div>
      </template>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">
import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import { computed, onMounted, ref } from "vue";
import { IArticle, TRoute } from "@/shared/types";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectViewSkeleton from "@/modules/projects/pages/ProjectView/ProjectViewSkeleton/ProjectViewSkeleton.vue";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import ArticlesServiceInstance from "@/shared/services/ArticlesService.ts";

const props = defineProps({
  articleId: {
    type: Number,
    default: null
  }
});

const router = useRouter();
const toaster = useToaster();
const { t } = useI18n();
const articlesService = ArticlesServiceInstance;

const loading = ref(true);
const currentArticle = ref<IArticle | null>(null);

const projectRoute = computed<TRoute>(() => {
  return {
    name: RouteNames.ProjectView,
    params: { projectId: currentArticle.value?.projectId }
  };
});

onMounted(async () => {
  try {
    loading.value = true;
    currentArticle.value = await articlesService.getArticle(props.articleId);
    loading.value = false;
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
});
</script>

<style scoped src="./ArticleView.scss" />
