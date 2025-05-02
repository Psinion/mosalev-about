<template>
  <PsiDialog
    :visible="visible"
    psi-form
    @update:visible="$emit('update:visible', $event)"
    @confirm="save"
    @open="onOpen"
  >
    <template #header>
      {{ project?.id ? t('projects.list.dialogEdit.titleEdit') : t('projects.list.dialogEdit.titleCreate') }}
    </template>

    <div class="project-edit-dialog">
      <PsiInput
        v-model="projectForm.title"
        :label="t('forms.title')"
        required
        :max-length="30"
      />
      <PsiTextarea
        v-model="projectForm.description"
        :label="t('forms.description')"
        required
      />
    </div>
  </PsiDialog>
</template>

<script setup lang="ts">
import PsiDialog from "@/shared/PsiUI/components/PsiDialog/PsiDialog.vue";
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import { PropType, ref } from "vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import ProjectsServiceInstance from "@/shared/services/ProjectsService.ts";
import { IProject } from "@/shared/types";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  project: {
    type: [Object, null] as PropType<IProject | null>,
    default: null
  }
});

const emit = defineEmits({
  "update:visible": (value: boolean) => true,
  "create": (value: IProject) => true,
  "edit": (value: IProject) => true
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

function onOpen() {
  const project = props.project;
  if (project) {
    projectForm.value = {
      title: project.title,
      description: project.description
    };
  }
  else {
    projectForm.value = {
      title: undefined,
      description: undefined
    };
  }
}

async function save() {
  try {
    if (!props.project?.id) {
      const savedProject = await projectsService.createProject({
        title: projectForm.value.title,
        description: projectForm.value.description
      });

      emit("create", savedProject);
    }
    else {
      const savedProject = await projectsService.updateProject(props.project.id, {
        title: projectForm.value.title,
        description: projectForm.value.description
      });

      emit("edit", savedProject);
    }
  }
  catch (error) {
    toaster.error(t("projects.list.dialogEdit.errorMessage"), error.message);
  }
}
</script>

<style scoped src="./ProjectEditDialog.scss" />
