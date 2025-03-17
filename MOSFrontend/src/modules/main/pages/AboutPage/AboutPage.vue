<template>
  <ContentLayout>
    <div class="about-page">
      <div class="actions">
        <PermissionChecker>
          <PsiButton
            tag="RouterLink"
            class="resume-list-button"
            :to="resumeListRoute"
          >
            {{ t('pages.resumeList') }}
          </PsiButton>
        </PermissionChecker>
        <PsiButton
          class="resume-button"
          tag="RouterLink"
          :to="resumeRoute"
          :disabled="!hasPinnedResume"
        >
          {{ t('about.resumeButton') }}
        </PsiButton>
      </div>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">
import { computed, onMounted, ref, watch } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { useUserStore } from "@/shared/stores/userStore.ts";
import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";

const userStore = useUserStore();
const resumesService = ResumesServiceInstance;
const { t } = useI18n();

const hasPinnedResume = ref(false);

const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});
const resumeRoute = computed(() => {
  return {
    name: RouteNames.ResumeView
  };
});

onMounted(async () => refresh());

async function refresh() {
  hasPinnedResume.value = await resumesService.hasPinnedResume();
}

watch(() => userStore.locale, () => refresh());
</script>

<style src="./AboutPage.scss" />
