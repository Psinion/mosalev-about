<template>
  <PsiDialog
    :visible="visible"
    hide-when-click-outside
    @update:visible="$emit('update:visible', $event)"
    @confirm="save"
  >
    <template #header>
      {{ t('projects.list.dialogDelete.title') }}
    </template>

    <div class="project-delete-dialog">
      {{ t('projects.list.dialogDelete.description') }}
    </div>
  </PsiDialog>
</template>

<script setup lang="ts">
import PsiDialog from "@/shared/PsiUI/components/PsiDialog/PsiDialog.vue";
import { useI18n } from "vue-i18n";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { PropType, toRef } from "vue";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  projectId: {
    type: [Number, null] as PropType<number | null>,
    default: null
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "delete": (value: number) => true
});

const toaster = useToaster();
const { t } = useI18n();
const projectsService = ProjectsServiceInstance;

const projectId = toRef(props, "projectId");

async function save() {
  if (!projectId.value) {
    return;
  }

  try {
    await projectsService.deleteProject(projectId.value);
    emit("delete", projectId.value);
  }
  catch (error) {
    toaster.error(t("projects.list.dialogDelete.errorMessage"), error.message);
  }
}
</script>

<style scoped src="./ProjectDeleteDialog.scss" />
