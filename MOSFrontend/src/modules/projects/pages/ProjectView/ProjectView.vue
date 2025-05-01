<template>
  <ContentLayout>
    <div class="project-view">
      <ProjectViewSkeleton v-if="loading" />
      <template v-else>
        <div class="actions" />
        <div class="content">
          <h2>{{ currentProject.title }}</h2>
          <div class="description caption-regular">
            {{ currentProject.description }}
          </div>
        </div>
        <div class="articles">
          <div
            class="empty-articles-placeholder caption-regular"
          >
            {{ t('projects.view.emptyArticlesPlaceholder') }}
          </div>
        </div>
      </template>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">

import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { onMounted, ref } from "vue";
import { IProject } from "@/shared/types";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectViewSkeleton from "@/modules/projects/pages/ProjectView/ProjectViewSkeleton/ProjectViewSkeleton.vue";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useRouter } from "vue-router";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";

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

const loading = ref(true);
const currentProject = ref<IProject>();

onMounted(async () => {
  try {
    loading.value = true;
    currentProject.value = await projectsService.getProject(props.projectId);
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

<style scoped src="./ProjectView.scss" />
