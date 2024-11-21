<template>
  <section class="resume-card">
    <RouterLink :to="resumeViewRoute">
      <header>
        <div class="title-block">
          <h3>{{ resume.title }}</h3>
          <LocaleBadge
            v-if="resume.pinnedToLocale !== null"
            :locale="resume.pinnedToLocale"
          />
        </div>
        <div class="actions">
          <PsiButton
            icon="pin"
            flat
            @click.prevent="pinResume"
          />
        </div>
      </header>
    </RouterLink>
  </section>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import { TResumeCompact } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { useUserStore } from "@/shared/stores/userStore.ts";
import LocaleBadge
  from "@/modules/resume/pages/ResumePage/components/ResumeList/components/LocaleBadge/LocaleBadge.vue";

const props = defineProps({
  resume: {
    type: Object as PropType<TResumeCompact>,
    required: true
  }
});

const userStore = useUserStore();
const resumesService = ResumesServiceInstance;

const resumeViewRoute = computed(() => {
  return {
    name: RouteNames.ResumeView,
    params: { resumeId: props.resume?.id }
  };
});

async function pinResume() {
  try {
    await resumesService.pinResume(props.resume!.id, {
      locale: userStore.locale
    });
  }
  catch (error) {

  }
}

</script>

<style scoped src="./ResumeCard.scss" />
