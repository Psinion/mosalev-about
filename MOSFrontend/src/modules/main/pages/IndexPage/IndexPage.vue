<template>
  <ContentLayout>
    <div class="index-page">
      <h2 class="font-color">
        {{ t('index.welcome') }}
      </h2>
      <div class="welcome-text typography-block body-regular">
        <div>
          <h3>{{ t('index.aboutWebsiteHeader') }}</h3>
          <p>
            {{ t('index.aboutWebsiteContent1') }} <strong>{{ t('index.aboutWebsiteContent2') }}</strong> {{ t('index.aboutWebsiteContent3') }} <a
              target="_blank"
              href="https://github.com/Psinion/mosalev-about"
            >GitHub</a>.
          </p>
        </div>
        <div v-if="currentResume">
          <h3>{{ t('about.header') }}</h3>
          <div>
            <p> {{ currentResume.about }}</p>
          </div>
        </div>
      </div>

      <div class="experience" />

      <PermissionChecker>
        <PsiButton
          v-if="currentResume"
          tag="RouterLink"
          class="action-button"
          :disabled="!currentResume?.id"
          :to="resumeEditRoute"
        >
          {{ t('resume.view.editButton') }}
        </PsiButton>
        <PsiButton
          v-else
          tag="RouterLink"
          class="resume-list-button"
          :to="resumeListRoute"
        >
          {{ t('pages.resumeList') }}
        </PsiButton>
      </PermissionChecker>
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { computed, onMounted, ref, watch } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { TResume } from "@/shared/types";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useRouter } from "vue-router";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useUserStore } from "@/shared/stores/userStore.ts";

const { t } = useI18n();
const router = useRouter();
const toaster = useToaster();
const userStore = useUserStore();
const resumesService = ResumesServiceInstance;

const currentResume = ref<TResume | null>(null);
const loading = ref(true);

const resumeEditRoute = computed(() => {
  return {
    name: RouteNames.ResumeEdit,
    params: { resumeId: currentResume.value?.id }
  };
});
const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});

onMounted(async () => refresh());
watch(() => userStore.locale, () => refresh());

async function refresh() {
  try {
    loading.value = true;
    currentResume.value = await resumesService.getPinnedResume();
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
  finally {
    loading.value = false;
  }
}
</script>

<style src="./IndexPage.scss" />
