<template>
  <FlatLayout align-horizontal="center">
    <div class="about-page">
      <div class="actions">
        <PsiButton
          :route="resumeRoute"
          disabled
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
import PsiButton from "@/shared/components/PsiButton/PsiButton.vue";
import { computed } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

const resumeRoute = computed(() => {
  return {
    name: RouteNames.ResumeView
  };
});
</script>

<style src="./AboutPage.scss" />
