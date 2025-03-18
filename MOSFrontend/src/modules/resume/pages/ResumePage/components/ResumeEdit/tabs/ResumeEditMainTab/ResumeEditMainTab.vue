<template>
  <article class="resume-edit-main-tab">
    <div class="content">
      <PsiForm
        v-if="!loading"
        v-slot="{ valid }"
        @submit="onSave"
      >
        <h3>Профиль</h3>
        <PsiInput
          v-model="form.title"
          label="Название"
          required
        />

        <PsiTextarea
          v-model="form.about"
          label="О себе"
          :rows="7"
          resizable="vertical"
        />

        <div class="actions">
          <PsiButton
            native-type="submit"
            :disabled="!valid"
          >
            Сохранить
          </PsiButton>
        </div>
      </PsiForm>
    </div>
  </article>
</template>

<script setup lang="ts">
import { onMounted, PropType, ref, toRef } from "vue";
import { TCreateResumeRequest, TUpdateResumeRequest } from "@/shared/services/base";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import { TResume } from "@/shared/types/resume.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { useResumeStore } from "@/modules/resume/stores/resumeStore.ts";

const props = defineProps({
  resume: {
    type: Object as PropType<TResume | null>,
    default: null
  },
  createMode: {
    type: Boolean,
    default: true
  }
});

const { t } = useI18n();
const toaster = useToaster();
const resumeStore = useResumeStore();

const loading = ref(false);

const currentResume = toRef(props, "resume");
const createMode = toRef(props, "createMode");

type TForm = {
  title?: string;
  about?: string | undefined;
};
const form = ref<TForm>({
  currencyType: false
});

onMounted(async () => refresh());

async function refresh() {
  if (!currentResume.value) {
    return;
  }

  const resume = currentResume.value;
  const fm = form.value;

  fm.title = resume.title;
  fm.about = resume.about ?? undefined;
}

async function onSave() {
  try {
    loading.value = true;
    const fm = form.value;

    if (createMode.value) {
      const resumeToSave: TCreateResumeRequest = {
        title: fm.title!,
        about: fm.about ?? null
      };

      await resumeStore.createResume(resumeToSave);
      toaster.success(t("resume.edit.toasterResumeCreateHeader"));
    }
    else {
      const resumeToSave: TUpdateResumeRequest = {
        id: currentResume.value!.id!,
        title: fm.title!,
        about: fm.about ?? null
      };

      await resumeStore.updateResume(resumeToSave);
      toaster.success(t("resume.edit.toasterResumeUpdateHeader"));
    }
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
  finally {
    loading.value = false;
  }
}
</script>

<style scoped src="./ResumeEditMainTab.scss" />
