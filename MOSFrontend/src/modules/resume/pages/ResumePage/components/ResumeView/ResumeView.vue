<template>
  <article class="resume-view">
    <div class="actions">
      <PsiButton
        tag="RouterLink"
        class="action-button"
        :to="resumeListRoute"
      >
        {{ t('resume.view.listButton') }}
      </PsiButton>
      <PsiButton
        v-if="resumeId"
        tag="RouterLink"
        class="action-button"
        :to="resumeEditRoute"
      >
        {{ t('resume.view.editButton') }}
      </PsiButton>
    </div>
    <div class="text body-regular">
      <div>
        <h2>Резюме</h2>
        email: {{ resume?.email }}
        Зарплата: {{ resume?.salary }}{{ resume?.currencyType }}
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { computed, onMounted, ref } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { TResume } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";

const props = defineProps({
  resumeId: {
    type: Number,
    default: null
  }
});

const { t } = useI18n();
const resumesService = ResumesServiceInstance;

const resume = ref<TResume | null>(null);

const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});
const resumeEditRoute = computed(() => {
  return {
    name: RouteNames.ResumeEdit,
    params: { resumeId: props.resumeId }
  };
});

onMounted(async () => refresh());

async function refresh() {
  if (!props.resumeId) {
    resume.value = await resumesService.getPinnedResume();
    return;
  }

  resume.value = await resumesService.getResume(props.resumeId);
}

</script>

<style scoped src="./ResumeView.scss" />
