<template>
  <ContentLayout>
    <div class="project-view">
      <div class="actions">
        <PsiButton
          tag="RouterLink"
          :to="projectsListRoute"
          class="action-button"
          icon="left"
        >
          {{ t('pages.projectsList') }}
        </PsiButton>
      </div>

      <ProjectViewSkeleton v-if="loadFirst" />
      <template v-else>
        <header class="header">
          <h2>{{ currentProject.title }}</h2>
        </header>

        <div class="date hint-regular tertiary">
          <PsiIcon icon="calendar" />
          <span>{{ dateString }}</span>
        </div>

        <div
          v-markdown="currentProject.description"
          class="description caption-regular"
        />

        <ArticlesList
          class="articles-block"
          :project-id="projectId"
        />
      </template>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">

import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { computed, onMounted, ref } from "vue";
import { IProject, TRoute } from "@/shared/types";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectViewSkeleton from "@/modules/projects/pages/ProjectView/ProjectViewSkeleton/ProjectViewSkeleton.vue";
import { ServerError } from "@/shared/utils/requests/errors.ts";
import { useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import ArticlesList from "@/modules/projects/shared/ArticlesList/ArticlesList.vue";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";
import { useDetailDateCreateUpdate2String } from "@/shared/composables/date.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";

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

const loadFirst = ref(true);
const currentProject = ref<IProject>();

const projectCreatedAt = computed(() => currentProject.value?.createdAt);
const projectUpdatedAt = computed(() => currentProject.value?.updatedAt);

const { dateString } = useDetailDateCreateUpdate2String(projectCreatedAt, projectUpdatedAt);

const projectsListRoute = computed<TRoute>(() => ({
  name: RouteNames.ProjectsList
}));

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
});
</script>

<style scoped src="./ProjectView.scss" />
