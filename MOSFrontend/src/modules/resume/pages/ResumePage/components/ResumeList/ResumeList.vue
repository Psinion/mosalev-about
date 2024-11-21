<template>
  <article class="resume-list">
    <div class="actions">
      <PsiButton
        tag="RouterLink"
        :to="resumeEditRoute"
      >
        Создать
      </PsiButton>
    </div>
    <div class="content">
      <h2>Список резюме</h2>
      <div class="resumes">
        <ResumeCard
          v-for="resume in resumesList"
          :key="resume.id"
          :resume="resume"
        />
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { TResumeCompact } from "@/shared/types/resume.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ResumeCard from "@/modules/resume/pages/ResumePage/components/ResumeList/ResumeCard/ResumeCard.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";

const resumesService = ResumesServiceInstance;
const toaster = useToaster();

const resumesList = ref<TResumeCompact[]>([]);

const resumeEditRoute = computed(() => {
  return {
    name: RouteNames.ResumeEdit
  };
});

onMounted(async () => refresh());

async function refresh() {
  try {
    resumesList.value = await resumesService.getResumesList();
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}

</script>

<style scoped src="./ResumeList.scss" />
