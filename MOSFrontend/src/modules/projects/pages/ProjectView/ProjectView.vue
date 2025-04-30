<template>
  <ContentLayout>
    <div class="project-view">
      <ProjectViewSkeleton v-if="loading" />
      <template v-else>
        <div class="actions" />
        <div class="content">
          <h2>{{ currentProject.title }}</h2>
          <div class="description">
            {{ currentProject.description }}
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

const props = defineProps({
  projectId: {
    type: Number,
    default: null
  }
});

const toaster = useToaster();
const projectsService = ProjectsServiceInstance;

const loading = ref(true);
const currentProject = ref<IProject>();

onMounted(async () => {
  try {
    loading.value = true;
    currentProject.value = await projectsService.getProject(props.projectId);
  }
  catch (error) {
    toaster.error("Произошла ошибка при получении проекта", error);
  }
  finally {
    loading.value = false;
  }
});

</script>

<style scoped src="./ProjectView.scss" />
