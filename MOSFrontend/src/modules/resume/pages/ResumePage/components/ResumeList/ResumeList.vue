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
      <h2>{{ t('pages.resumeList') }}</h2>
      <div
        v-if="!loading"
        class="resumes"
      >
        <ResumeCard
          v-for="resume in resumesList"
          :key="resume.id"
          :resume="resume"
          @pin="refresh"
        />
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { computed, onMounted, ref } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { ServerError } from "@/shared/utils/requests/errors.ts";
import { TResumeCompact } from "@/shared/types/resume.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import ResumeCard from "@/modules/resume/pages/ResumePage/components/ResumeList/components/ResumeCard/ResumeCard.vue";
import { useI18n } from "vue-i18n";

const resumesService = ResumesServiceInstance;
const toaster = useToaster();
const { t } = useI18n();

const loading = ref(false);
const resumesList = ref<TResumeCompact[]>([]);

const resumeEditRoute = computed(() => {
  return {
    name: RouteNames.ResumeEdit
  };
});

onMounted(async () => refresh());

async function refresh() {
  try {
    loading.value = true;
    resumesList.value = await resumesService.getResumesList();
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
  finally {
    loading.value = false;
  }
}

</script>

<style scoped src="./ResumeList.scss" />
