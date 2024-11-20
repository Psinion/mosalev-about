<template>
  <section class="resume-card">
    <RouterLink :to="resumeViewRoute">
      <h3>{{ resume.title }}</h3>
    </RouterLink>
  </section>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import { TResumeCompact } from "@/shared/types/resume.ts";

const props = defineProps({
  resume: {
    type: Object as PropType<TResumeCompact>,
    required: true
  }
});

const resumeViewRoute = computed(() => {
  return {
    name: RouteNames.ResumeView,
    params: { resumeId: props.resume?.id }
  };
});

</script>

<style scoped src="./ResumeCard.scss" />
