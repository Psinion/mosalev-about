<template>
  <article class="resume-edit">
    <div class="actions">
      <PsiButton
        tag="RouterLink"
        class="action-button"
        :to="resumeListRoute"
      >
        {{ t('resume.edit.listButton') }}
      </PsiButton>
      <PsiButton
        tag="RouterLink"
        class="action-button"
        :to="resumeViewRoute"
      >
        {{ t('resume.edit.viewButton') }}
      </PsiButton>
    </div>
    <div class="content">
      <h2>{{ createMode ? t('resume.edit.headerCreate') : t('resume.edit.headerEdit') }}</h2>
      <div class="actions" />
      <PsiForm
        v-slot="{ valid }"
        @submit="onSave"
      >
        <PsiInput
          v-model="title"
          label="Название"
          required
        />
        <PsiInput
          v-model="email"
          label="Email"
          required
        />
        <div class="salary-input">
          <PsiInputNumeric
            v-model="salary"
            label="Зарплата"
            :min="0"
            required
          />
          <PsiToggle
            v-model="currencyType"
            inactive-label="₽"
            active-label="$"
          />
        </div>
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
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { computed, onMounted, PropType, ref } from "vue";
import { TCreateResumeRequest } from "@/shared/services/base";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiToggle from "@/shared/PsiUI/components/PsiToggle/PsiToggle.vue";
import PsiInputNumeric from "@/shared/PsiUI/components/PsiInputNumeric/PsiInputNumeric.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import { CurrencyType } from "@/shared/types/resume.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import { RouteNames } from "@/router/routeNames.ts";

const props = defineProps({
  resumeId: {
    type: Number as PropType<number | null>,
    default: null
  }
});

const { t } = useI18n();
const toaster = useToaster();
const resumesService = ResumesServiceInstance;

const title = ref<string | null>();
const email = ref<string | null>();
const salary = ref<number | null>();
const currencyType = ref<boolean>(false);

const createMode = computed(() => props.resumeId === null);

const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});
const resumeViewRoute = computed(() => {
  return {
    name: RouteNames.ResumeView,
    params: { resumeId: props.resumeId }
  };
});

onMounted(async () => refresh());

async function refresh() {
  if (!props.resumeId) {
    return;
  }

  const resume = await resumesService.getResume(props.resumeId);

  title.value = resume.title;
  email.value = resume.email;
  salary.value = resume.salary;
  currencyType.value = resume.currencyType === CurrencyType.Ruble;
}

async function onSave() {
  const resumeToSave: TCreateResumeRequest = {
    title: title.value!,
    email: email.value!,
    salary: salary.value!,
    currencyType: currencyType.value ? CurrencyType.Ruble : CurrencyType.Dollar
  };

  try {
    if (createMode.value) {
      await resumesService.createResume(resumeToSave);
      toaster.success(t("resume.edit.toasterResumeCreateHeader"));
    }
    else {
      await resumesService.updateResume(props.resumeId!, resumeToSave);
      toaster.success(t("resume.edit.toasterResumeUpdateHeader"));
    }
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(error.header, error.message);
    }
  }
}
</script>

<style scoped src="./ResumeEdit.scss" />
