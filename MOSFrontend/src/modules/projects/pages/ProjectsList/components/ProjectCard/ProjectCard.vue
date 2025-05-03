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
            v-tooltip="project.visible ? t('forms.hide') : t('forms.show')"
            flat
            :icon="project.visible ? 'eye' : 'eye-crossed'"
            @click.prevent="$emit('changeVisibility', project, !project.visible)"
          />
          <PsiButton
            v-tooltip="t('forms.edit')"
            flat
            icon="edit"
            @click.prevent="$emit('edit', project)"
          />
          <PsiButton
            v-tooltip="t('forms.delete')"
            flat
            icon="trash-box"
            @click.prevent="$emit('delete', project)"
          />
        </PermissionChecker>
      </div>
    </header>
    <div
      v-markdown
      class="description caption-regular"
    >
      {{ project.description }}
    </div>
    <footer>
      <div
        v-tooltip="{
          text: dateUpdate,
          width: '160px'
        }"
        class="hint-regular tertiary"
      >
        {{ dateCreate }}
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
import PermissionChecker from "@/shared/components/PermissionChecker/PermissionChecker.vue";
import { useI18n } from "vue-i18n";

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

const { t } = useI18n();

const dateUpdate = computed(() => {
  const date = props.project.updatedAt;
  return date ? formatDate(date, "YYYY.MM.DD HH:mm") : "";
});

const dateCreate = computed(() => {
  const date = props.project.createdAt;
  const formattedDate = formatDate(date, "YYYY.MM.DD HH:mm");
  return dateUpdate.value ? `${formattedDate} (${t("forms.editMark")})` : formattedDate;
});

const projectViewRoute = computed(() => {
  return {
    name: RouteNames.ProjectView,
    params: { projectId: props.project?.id }
  };
});
</script>

<style scoped src="./ProjectCard.scss" />
