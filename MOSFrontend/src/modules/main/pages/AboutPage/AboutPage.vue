<template>
  <FlatLayout align-horizontal="center">
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
          :to="resumeRoute"
        >
          {{ t('about.resumeButton') }}
        </PsiButton>
      </div>
      <div class="text typography-block body-regular">
        <div>
          <h3>{{ t('about.header') }}</h3>
          <p> {{ t('about.content1') }}<strong>{{ t('about.content2') }}</strong>{{ t('about.content3') }}</p>
          <p>{{ t('about.content4') }}</p>
          <p>{{ t('about.content5') }}</p>
        </div>
      </div>
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">
import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
import { computed } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";

const { t } = useI18n();

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
</script>

<style src="./AboutPage.scss" />
