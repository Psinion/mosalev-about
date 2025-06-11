<template>
  <ContentLayout>
    <div class="article-edit">
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

          <PsiButton
            v-if="currentArticle?.id"
            tag="RouterLink"
            :to="articleViewRoute"
            class="action-button"
          >
            {{ t('forms.view') }}
          </PsiButton>
        </div>

        <div class="content">
          <h2>{{ articleId ? currentArticle.title : t('articles.edit.titleCreate') }}</h2>

          <PsiForm
            v-slot="{valid}"
            class="article-form"
            @submit="submit"
          >
            <div class="actions">
              <PermissionChecker>
                <PsiButton
                  native-type="submit"
                  class="action-button"
                  :disabled="!valid || !canSave"
                >
                  {{ t('forms.save') }}
                </PsiButton>
              </PermissionChecker>
            </div>

            <PsiInput
              v-model="articleForm.title"
              :label="t('forms.title')"
              required
            />
            <PsiMarkdownEditor
              v-model="articleForm.description"
              :label="t('forms.description')"
              height="80vh"
              required
              @update:model-value="canSave = true"
            />
          </PsiForm>
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
import { ServerError } from "@/shared/utils/requests/errors.ts";
import { useRoute, useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import ArticlesServiceInstance from "@/shared/services/ArticlesService.ts";
import { checkPositiveNumber } from "@/shared/utils/helpers";
import PsiMarkdownEditor from "@/shared/PsiUI/components/PsiMarkdownEditor/PsiMarkdownEditor.vue";

const props = defineProps({
  articleId: {
    type: Number,
    default: null
  }
});

type TForm = {
  title?: string;
  description?: string;
};

const route = useRoute();
const router = useRouter();
const toaster = useToaster();
const { t } = useI18n();
const articlesService = ArticlesServiceInstance;

const articleForm = ref<TForm>({
  title: undefined,
  description: undefined
});
const projectId = ref<number | null>(null);

const loading = ref(true);
const currentArticle = ref<IArticle | null>(null);

const canSave = ref(false);

const projectRoute = computed<TRoute>(() => {
  return {
    name: RouteNames.ProjectView,
    params: { projectId: currentArticle.value?.projectId }
  };
});

const articleViewRoute = computed(() => {
  return {
    name: RouteNames.ArticleView,
    params: { articleId: currentArticle.value?.id ?? props.articleId }
  };
});

onMounted(async () => {
  const project = route.query["project"] as string;
  if (checkPositiveNumber(project)) {
    projectId.value = +project;
  }

  if (props.articleId) {
    try {
      loading.value = true;
      const article = await articlesService.getArticle(props.articleId);

      articleForm.value = {
        title: article.title,
        description: article.description
      };

      projectId.value = article.projectId;
      currentArticle.value = article;
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
  }

  loading.value = false;
});

async function submit() {
  try {
    const form = articleForm.value;

    if (!props.articleId) {
      currentArticle.value = await articlesService.createArticle({
        title: form.title!,
        description: form.description!,
        projectId: projectId.value
      });

      await router.replace({
        name: RouteNames.ArticleEdit,
        params: { articleId: currentArticle.value.id }
      });
    }
    else {
      currentArticle.value = await articlesService.updateArticle(currentArticle.value.id, {
        title: form.title!,
        description: form.description!,
        projectId: projectId.value
      });
    }

    canSave.value = false;
    toaster.success(t("toaster.successSaveHeader"));
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}

</script>

<style scoped src="./ArticleEdit.scss" />
