<template>
  <ContentLayout>
    <div class="article-edit">
      <ProjectViewSkeleton v-if="loading" />
      <template v-else>
        <div class="content">
          <h2>{{ articleId ? t('articles.edit.titleEdit') : t('articles.edit.titleCreate') }}</h2>

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
                  :disabled="!valid"
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
            <PsiTextarea
              v-model="articleForm.description"
              :label="t('forms.description')"
              required
            />
          </PsiForm>
        </div>
      </template>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">

import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import { onMounted, ref } from "vue";
import { IArticle } from "@/shared/types";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectViewSkeleton from "@/modules/projects/pages/ProjectView/ProjectViewSkeleton/ProjectViewSkeleton.vue";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useRoute, useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import ArticlesServiceInstance from "@/shared/services/ArticlesService.ts";
import { checkPositiveNumber } from "@/shared/utils/helpers";

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

const loading = ref(false);
const currentArticle = ref<IArticle | null>(null);

onMounted(async () => {
  const project = route.query["project"] as string;
  if (checkPositiveNumber(project)) {
    projectId.value = +project;
  }

  return;

  try {
    loading.value = true;
    currentProject.value = await projectsService.getProject(props.articleId);
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

async function submit() {
  try {
    const form = articleForm.value;

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
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}

</script>

<style scoped src="./ArticleEdit.scss" />
