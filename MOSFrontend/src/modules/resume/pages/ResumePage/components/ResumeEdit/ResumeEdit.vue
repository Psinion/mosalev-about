<template>
  <article class="resume-edit">
    <div class="actions">
      <PsiButton
        tag="RouterLink"
        class="action-button"
        :to="resumeListRoute"
      >
        {{ t('resume.edit.listButton') }}
      </PsiButton>
      <PsiButton
        tag="RouterLink"
        class="action-button"
        :to="resumeViewRoute"
      >
        {{ t('resume.edit.viewButton') }}
      </PsiButton>
    </div>

    <h2>{{ createMode ? t('resume.edit.headerCreate') : t('resume.edit.headerEdit') }}</h2>

    <div class="tabs">
      <PsiButton
        :flat="activeTab === 'main'"
        class="tab-button"
        :disabled="activeTab === 'main'"
        @click="activeTab = 'main'"
      >
        {{ t('resume.edit.tabMain') }}
      </PsiButton>
      <PsiButton
        :flat="activeTab === 'job-exp'"
        class="tab-button"
        :disabled="createMode || activeTab === 'job-exp'"
        @click="activeTab = 'job-exp'"
      >
        {{ t('resume.edit.tabWorkExp') }}
      </PsiButton>
    </div>

    <template v-if="loading">
      <PsiSkeleton
        height="20px"
        width="100%"
      />
      <PsiSkeleton
        height="20px"
        width="80%"
      />
      <PsiSkeleton
        height="20px"
        width="80%"
      />
      <PsiSkeleton
        height="20px"
        width="50%"
      />
    </template>
    <template v-else>
      <ResumeEditMainTab
        v-if="activeTab === 'main'"
        :resume="currentResume"
        :create-mode="createMode"
      />
      <ResumeEditJobExpTab
        v-else-if="activeTab === 'job-exp'"
        :resume="currentResume"
      />
    </template>
  </article>
</template>

<script setup lang="ts">
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { computed, onMounted, ref } from "vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { TResume } from "@/shared/types/resume.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import { RouteNames } from "@/router/routeNames.ts";
import ResumeEditMainTab
  from "@/modules/resume/pages/ResumePage/components/ResumeEdit/tabs/ResumeEditMainTab/ResumeEditMainTab.vue";
import PsiSkeleton from "@/shared/PsiUI/components/PsiSkeleton/PsiSkeleton.vue";
import ResumeEditJobExpTab
  from "@/modules/resume/pages/ResumePage/components/ResumeEdit/tabs/ResumeEditWorkExpTab/ResumeEditWorkExpTab.vue";

type TResumeTabs = "main" | "job-exp";

const props = defineProps({
  resumeId: {
    type: Number,
    default: 0
  }
});

const { t } = useI18n();
const toaster = useToaster();
const resumesService = ResumesServiceInstance;

const loading = ref(false);

const currentResume = ref<TResume | null>(null);

const createMode = computed(() => props.resumeId === 0);

const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});
const resumeViewRoute = computed(() => {
  return {
    name: RouteNames.ResumeView,
    params: { resumeId: props.resumeId }
  };
});

const activeTab = ref<TResumeTabs>("main");

onMounted(async () => refresh());

async function refresh() {
  if (!props.resumeId) {
    return;
  }

  try {
    loading.value = true;
    const resume = await resumesService.getResume(props.resumeId);
    currentResume.value = resume;
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

<style scoped src="./ResumeEdit.scss" />
