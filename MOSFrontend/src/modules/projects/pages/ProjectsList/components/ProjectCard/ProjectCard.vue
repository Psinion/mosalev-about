<template>
  <RouterLink
    :to="projectViewRoute"
    class="project-card"
  >
    <header class="header">
      <h3>{{ project.title }}</h3>

      <div
        class="actions"
      >
        <PermissionChecker>
          <PsiButton
            flat
            :icon="project.visible ? 'eye' : 'eye-crossed'"
            @click.prevent="$emit('changeVisibility', project, !project.visible)"
          />
          <PsiButton
            flat
            icon="edit"
            @click.prevent="$emit('edit', project)"
          />
          <PsiButton
            flat
            icon="trash-box"
            @click.prevent="$emit('delete', project)"
          />
        </PermissionChecker>
      </div>
    </header>
    <div class="description caption-regular">
      {{ project.description }}
    </div>
    <footer>
      <div class="hint-regular tertiary">
        {{ dateUpdate }}
      </div>
    </footer>
  </RouterLink>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { IProjectCompact } from "@/shared/types";
import { formatDate } from "@/shared/utils/dateHelpers.ts";
import { RouteNames } from "@/router/routeNames.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { useUserStore } from "@/shared/stores/userStore.ts";
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";

const props = defineProps({
  project: {
    type: Object as PropType<IProjectCompact>,
    required: true
  }
});

const emit = defineEmits({
  changeVisibility: (project: IProjectCompact, visibility: boolean) => true,
  edit: (value: IProjectCompact) => true,
  delete: (value: IProjectCompact) => true
});

const userStore = useUserStore();

const currentUser = computed(() => userStore.user);

const dateUpdate = computed(() => {
  const date = props.project.updatedAt ?? props.project.createdAt;
  return `${formatDate(date, "YYYY.MM.DD HH:mm")}`;
});

const projectViewRoute = computed(() => {
  return {
    name: RouteNames.ProjectView,
    params: { projectId: props.project?.id }
  };
});
</script>

<style scoped src="./ProjectCard.scss" />
