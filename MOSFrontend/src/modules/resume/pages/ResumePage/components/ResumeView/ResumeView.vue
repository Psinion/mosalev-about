<template>
  <article class="resume-view">
    <div class="actions">
      <PsiButton
        class="action-button"
        :route="resumeListRoute"
      >
        {{ t('resume.view.listButton') }}
      </PsiButton>
      <PsiButton
        class="action-button"
        :route="resumeEditRoute"
      >
        {{ t('resume.view.editButton') }}
      </PsiButton>
    </div>
    <div class="text body-regular">
      <div>
        <h2>Резюме</h2>
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { computed } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";

const { t } = useI18n();

const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});
const resumeEditRoute = computed(() => {
  return {
    name: RouteNames.ResumeEdit
  };
});

</script>

<style src="./ResumeView.scss" />
