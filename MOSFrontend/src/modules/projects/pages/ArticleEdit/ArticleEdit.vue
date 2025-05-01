<template>
  <ContentLayout>
    <div class="article-edit">
      <ProjectViewSkeleton v-if="loading" />
      <template v-else>
        <div class="content">
          <h2>{{ t('articles.edit.title') }}</h2>

          <PsiForm
            v-slot="{valid}"
            class="article-form"
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

            <PsiInput :label="t('forms.title')" />
            <PsiTextarea :label="t('forms.description')" />
          </PsiForm>
        </div>
      </template>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">

import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { onMounted, ref } from "vue";
import { IArticle, IProject } from "@/shared/types";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectViewSkeleton from "@/modules/projects/pages/ProjectView/ProjectViewSkeleton/ProjectViewSkeleton.vue";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
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

const loading = ref(false);
const currentArticle = ref<IArticle>();

onMounted(async () => {
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

</script>

<style scoped src="./ArticleEdit.scss" />
