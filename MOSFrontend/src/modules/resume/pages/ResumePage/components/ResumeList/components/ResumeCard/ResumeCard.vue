<template>
  <section class="resume-card">
    <RouterLink :to="resumeViewRoute">
      <header>
        <div class="title-block">
          <h3>{{ resume.title }}</h3>
          <LocaleBadge
            v-if="resumePinned"
            :locale="resume.pinnedToLocale!"
          />
        </div>
        <div class="actions">
          <PsiButton
            :icon="resumePinned ? 'pin' : 'pin-filled'"
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
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";

const props = defineProps({
  resume: {
    type: Object as PropType<TResumeCompact>,
    required: true
  }
});

const toaster = useToaster();
const userStore = useUserStore();
const resumesService = ResumesServiceInstance;

const resumePinned = computed(() => props.resume.pinnedToLocale !== null);

const resumeViewRoute = computed(() => {
  return {
    name: RouteNames.ResumeView,
    params: { resumeId: props.resume?.id }
  };
});

async function pinResume() {
  if (resumePinned.value) {

  }
  else {
    try {
      const locale = userStore.locale;
      await resumesService.pinResume(props.resume!.id, {
        locale: locale
      });
      toaster.success(`Резюме закреплено к локали "${locale}"`);
    }
    catch (error) {
      if (error instanceof ServerError) {
        toaster.error(error.header, error.message);
      }
    }
  }
}

</script>

<style scoped src="./ResumeCard.scss" />
