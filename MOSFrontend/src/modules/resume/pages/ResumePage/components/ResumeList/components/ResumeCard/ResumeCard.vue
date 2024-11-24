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
      <div>
        <span
          v-if="dateUpdate"
          class="hint-regular"
        >{{ dateUpdate }}</span>
      </div>
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
import { AppLocale } from "@/shared/enums/common.ts";
import { formatDate } from "@/shared/utils/dateHelpers.ts";

const props = defineProps({
  resume: {
    type: Object as PropType<TResumeCompact>,
    required: true
  }
});

const emit = defineEmits({
  pin: () => true
});

const toaster = useToaster();
const userStore = useUserStore();
const resumesService = ResumesServiceInstance;

const resumePinned = computed(() => props.resume.pinnedToLocale !== null);
const dateUpdate = computed(() => {
  const resume = props.resume;
  if (resume?.dateUpdate) {
    return `Дата изменения: ${formatDate(resume.dateUpdate, "DD.MM.YYYY HH:mm")}`;
  }

  return null;
});

const resumeViewRoute = computed(() => {
  return {
    name: RouteNames.ResumeView,
    params: { resumeId: props.resume?.id }
  };
});

async function pinResume() {
  try {
    if (resumePinned.value) {
      await resumesService.unpinResume(props.resume!.id);
      toaster.success("Резюме откреплено");
    }
    else {
      const locale = userStore.locale;
      await resumesService.pinResume(props.resume!.id);
      toaster.success(`Резюме закреплено в локали "${AppLocale[locale]}"`);
    }
    emit("pin");
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}

</script>

<style scoped src="./ResumeCard.scss" />
