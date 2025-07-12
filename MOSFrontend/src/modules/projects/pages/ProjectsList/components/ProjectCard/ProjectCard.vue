<template>
  <component
    :is="route ? 'RouterLink' : 'span'"
    :to="projectViewRoute"
    class="project-card"
    :class="[{hidden: !project.visible, disabled: disabled}]"
    draggable="false"
  >
    <header class="header">
      <h3 class="header-title">
        {{ project.title }}
      </h3>

      <div
        class="actions"
      >
        <slot name="actions" />
      </div>
    </header>
    <div
      v-markdown="project.description"
      class="description caption-regular"
    />
    <footer>
      <div class="date hint-regular tertiary">
        <PsiIcon icon="calendar" />
        <span>{{ dateCreateString }}</span>
      </div>
    </footer>
  </component>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { IProjectCompact, TRoute } from "@/shared/types";
import { RouteNames } from "@/router/routeNames.ts";
import { useShortDateCreateUpdate2String } from "@/shared/composables/date.ts";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";

const props = defineProps({
  project: {
    type: Object as PropType<IProjectCompact>,
    required: true
  },
  disabled: {
    type: Boolean,
    default: false
  },
  route: {
    type: Object as PropType<TRoute>,
    default: null
  }
});

const projectCreatedAt = computed(() => props.project.createdAt);
const projectUpdatedAt = computed(() => props.project.updatedAt);

const { dateCreateString, dateUpdateString } = useShortDateCreateUpdate2String(projectCreatedAt, projectUpdatedAt);

const projectViewRoute = computed(() => {
  return {
    name: RouteNames.ProjectView,
    params: { projectId: props.project?.id }
  };
});
</script>

<style scoped src="./ProjectCard.scss" />
