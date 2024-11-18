<template>
  <article class="resume-edit">
    <div class="actions">
      <PsiButton
        class="action-button"
        :route="resumeListRoute"
      >
        {{ t('resume.edit.listButton') }}
      </PsiButton>
      <PsiButton
        class="action-button"
        :route="resumeViewRoute"
      >
        {{ t('resume.edit.viewButton') }}
      </PsiButton>
    </div>
    <div class="content">
      <h2>Создание резюме</h2>
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
            type="submit"
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
import { computed, ref } from "vue";
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

const { t } = useI18n();
const toaster = useToaster();
const resumesService = ResumesServiceInstance;

const title = ref<string | null>();
const email = ref<string | null>();
const salary = ref<number | null>();
const currencyType = ref<boolean>(false);

const resumeListRoute = computed(() => {
  return {
    name: RouteNames.ResumeList
  };
});
const resumeViewRoute = computed(() => {
  return {
    name: RouteNames.ResumeView
  };
});

async function onSave() {
  const resumeToSave: TCreateResumeRequest = {
    title: title.value!,
    email: email.value!,
    salary: salary.value!,
    currencyType: currencyType.value ? CurrencyType.Ruble : CurrencyType.Dollar
  };

  try {
    await resumesService.createResume(resumeToSave);
    toaster.success("Резюме создано");
  }
  catch (error) {
    if (error instanceof ServerError) {
      toaster.error(t("toaster.commonErrorHeader"), error.message);
    }
  }
}
</script>

<style src="./ResumeEdit.scss" />
