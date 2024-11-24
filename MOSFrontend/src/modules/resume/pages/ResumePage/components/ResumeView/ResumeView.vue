<template>
  <article class="resume-view">
    <div class="actions">
      <PermissionChecker>
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
          :disabled="!currentResume?.id"
          :to="resumeEditRoute"
        >
          {{ t('resume.view.editButton') }}
        </PsiButton>
      </PermissionChecker>
    </div>
    <div class="content body-regular">
      <div
        v-if="!loading"
        class="content"
      >
        <div class="profile">
          <div class="fio">
            <h3>{{ currentResume?.lastName }} {{ currentResume?.firstName }}</h3>
            <span>{{ currentResume?.title }}</span>
            <span v-if="currentResume?.salary">{{ currentResume.salary }} {{ currentResume.currencyType ? '$' : '₽' }}</span>
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
                :href="`mailto:${currentResume?.email}`"
              >
                <PsiIcon
                  :scale="0.9"
                  icon="email"
                />{{ currentResume?.email }}
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
          {{ currentResume?.about }}
        </div>

        <footer class="hint-regular">
          {{ dateUpdate }}
        </footer>
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import { computed, onMounted, ref, watch } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { TResume } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";
import { formatDate } from "@/shared/utils/dateHelpers.ts";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { useRouter } from "vue-router";

const props = defineProps({
  resumeId: {
    type: Number,
    default: null
  }
});

const router = useRouter();
const toaster = useToaster();
const { t } = useI18n();
const userStore = useUserStore();
const resumesService = ResumesServiceInstance;

const currentResume = ref<TResume | null>(null);
const loading = ref(true);

const dateUpdate = computed(() => {
  const resume = currentResume.value;
  if (resume?.dateUpdate) {
    return `${t("resume.view.footerDateUpdate")} ${formatDate(resume.dateUpdate, "DD.MM.YYYY HH:mm")}`;
  }

  return null;
});

const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});
const resumeEditRoute = computed(() => {
  return {
    name: RouteNames.ResumeEdit,
    params: { resumeId: currentResume.value?.id }
  };
});

onMounted(async () => refresh());

async function refresh() {
  try {
    loading.value = true;
    if (!props.resumeId) {
      currentResume.value = await resumesService.getPinnedResume();
    }
    else {
      currentResume.value = await resumesService.getResume(props.resumeId);
    }
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

watch(() => userStore.locale, () => refresh());
</script>

<style scoped src="./ResumeView.scss" />
