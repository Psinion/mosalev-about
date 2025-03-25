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
        <div
          v-if="currentResume"
          class="resume"
        >
          <h3>{{ t('about.header') }}</h3>
          <div>
            <p> {{ currentResume.about }}</p>
          </div>

          <div
            v-if="currentResume.skills.length > 0"
            class="resume-skills-block"
          >
            <h4>{{ t('about.confidence') }}</h4>
            <div
              v-if="skillsHigh.length > 0"
              class="resume-skills high"
            >
              <SkillChip
                v-for="skill in skillsHigh"
                :key="skill.id"
                :name="skill.name"
              />
            </div>
            <div
              v-if="skillsMedium.length > 0"
              class="resume-skills medium"
            >
              <SkillChip
                v-for="skill in skillsMedium"
                :key="skill.id"
                :name="skill.name"
              />
            </div>
            <div
              v-if="skillsLow.length > 0"
              class="resume-skills low"
            >
              <SkillChip
                v-for="skill in skillsLow"
                :key="skill.id"
                :name="skill.name"
              />
            </div>
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
import { ResumeSkillLevelType, TResume } from "@/shared/types";
import { useUserStore } from "@/shared/stores/userStore.ts";
import SkillChip from "@/shared/components/SkillChip/SkillChip.vue";

const { t } = useI18n();
const userStore = useUserStore();
const resumesService = ResumesServiceInstance;

const currentResume = ref<TResume | null>(null);
const loading = ref(true);

const skillsHigh = computed(() => currentResume.value?.skills.filter(x => x.level === ResumeSkillLevelType.High) ?? []);
const skillsMedium = computed(() => currentResume.value?.skills.filter(x => x.level === ResumeSkillLevelType.Medium) ?? []);
const skillsLow = computed(() => currentResume.value?.skills.filter(x => x.level === ResumeSkillLevelType.Low) ?? []);

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
  catch {
    currentResume.value = null;
  }
  finally {
    loading.value = false;
  }
}
</script>

<style src="./IndexPage.scss" />
