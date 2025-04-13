<template>
  <PsiDialog
    :visible="visible"
    psi-form
    @update:visible="$emit('update:visible', $event)"
    @confirm="save"
  >
    <template #header>
      {{ t('projects.list.dialogEdit.titleCreate') }}
    </template>

    <div class="project-dialog">
      <PsiInput
        v-model="projectForm.title"
        :label="t('forms.title')"
        required
      />
      <PsiTextarea
        v-model="projectForm.description"
        :label="t('forms.description')"
        required
        resizable="vertical"
      />
    </div>
  </PsiDialog>
</template>

<script setup lang="ts">
import PsiDialog from "@/shared/PsiUI/components/PsiDialog/PsiDialog.vue";
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import { ref } from "vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true
});

type TForm = {
  title?: string;
  description?: string;
};

const toaster = useToaster();
const { t } = useI18n();
const projectsService = ProjectsServiceInstance;

const projectForm = ref<TForm>({
  title: undefined,
  description: undefined
});

async function save() {
  try {
    await projectsService.createProject({
      title: projectForm.value.title,
      description: projectForm.value.description
    });
  }
  catch (error) {
    toaster.error(t("projects.list.dialogEdit.errorMessage"), error.message);
  }
}
</script>

<style scoped src="./ProjectDialog.scss" />
