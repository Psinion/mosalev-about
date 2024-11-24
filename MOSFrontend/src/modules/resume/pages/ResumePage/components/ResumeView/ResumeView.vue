<template>
  <article class="resume-view">
    <PermissionChecker>
      <div class="actions">
        <PsiButton
          tag="RouterLink"
          class="action-button"
          :to="resumeListRoute"
        >
          {{ t('resume.view.listButton') }}
        </PsiButton>
        <PsiButton
          tag="RouterLink"
          class="action-button"
          :disabled="!resume?.id"
          :to="resumeEditRoute"
        >
          {{ t('resume.view.editButton') }}
        </PsiButton>
      </div>
    </PermissionChecker>
    <div class="content body-regular">
      <LoadingSpinner v-show="loading" />
      <div
        v-if="!loading"
        class="content"
      >
        <div class="profile">
          <div class="fio">
            <h3>{{ resume?.lastName }} {{ resume?.firstName }}</h3>
            <span>{{ resume?.title }}</span>
            <span v-if="resume?.salary">{{ resume.salary }} {{ resume.currencyType ? '$' : '₽' }}</span>
          </div>
          <div class="contacts">
            <div class="contact-item">
              <RouterLink
                class=" hint-regular"
                to="/"
              >
                <img
                  class="website-logo"
                  src="@/assets/images/logo.svg"
                  alt="logo"
                >
                mosalev.su
              </RouterLink>
            </div>
            <div class="contact-item">
              <a
                class="contact-item hint-regular"
                :href="`mailto:${resume?.email}`"
              >
                <PsiIcon
                  :scale="0.9"
                  icon="email"
                />{{ resume?.email }}
              </a>
            </div>
            <div class="contact-item">
              <a
                class="contact-item hint-regular"
                href="https://t.me/mosalev_daniel"
                target="_blank"
              >
                <PsiIcon
                  :scale="0.9"
                  icon="telegram"
                />mosalev_daniel
              </a>
            </div>
            <div class="contact-item">
              <a
                class="contact-item hint-regular"
                href="https://github.com/Psinion"
                target="_blank"
              >
                <PsiIcon
                  :scale="0.9"
                  icon="github"
                />github.com/Psinion
              </a>
            </div>
          </div>
        </div>

        <div class="about typography-block">
          {{ resume?.about }}
        </div>
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
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import LoadingSpinner from "@/shared/components/LoadingSpinner/LoadingSpinner.vue";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";

const props = defineProps({
  resumeId: {
    type: Number,
    default: null
  }
});

const toaster = useToaster();
const { t } = useI18n();
const resumesService = ResumesServiceInstance;

const resume = ref<TResume | null>(null);
const loading = ref(false);

const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});
const resumeEditRoute = computed(() => {
  return {
    name: RouteNames.ResumeEdit,
    params: { resumeId: resume.value?.id }
  };
});

onMounted(async () => {
  try {
    loading.value = true;
    await refresh();
    loading.value = false;
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
});

async function refresh() {
  if (!props.resumeId) {
    resume.value = await resumesService.getPinnedResume();
    return;
  }

  resume.value = await resumesService.getResume(props.resumeId);
}

</script>

<style scoped src="./ResumeView.scss" />
