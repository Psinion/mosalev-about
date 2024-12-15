<template>
  <article class="resume-edit-company-entry">
    <PsiForm
      v-model:auto-submit-timer-stop="autoSubmitStop"
      class="form"
      :auto-submit-timer="2000"
      @valid="valid = $event"
      @submit="submit"
    >
      <div class="company-input">
        <PsiInput
          v-model="formCompany"
          label="Организация"
          required
          @focus="onFormFocus"
          @blur="onFormBlur"
        />
        <PsiButton
          flat
          icon="close"
          @click="removeCompany(companyEntry)"
        />
      </div>
      <PsiInput
        v-model="formWebSiteUrl"
        label="Сайт"
        @focus="onFormFocus"
        @blur="onFormBlur"
      />
      <PsiTextarea
        v-model="formDescription"
        label="Описание"
        resizable="vertical"
        @focus="onFormFocus"
        @blur="onFormBlur"
      />
    </PsiForm>
  </article>
</template>

<script setup lang="ts">
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { computed, onMounted, PropType, ref, toRef } from "vue";
import { TResumeCompanyEntry } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import ResumesCompanyEntriesServiceInstance from "@/shared/services/ResumeCompanyEntriesService.ts";

const props = defineProps({
  companyEntry: {
    type: Object as PropType<TResumeCompanyEntry>,
    required: true
  }
});

const emit = defineEmits({
  "update:modelValue": (value: TResumeCompanyEntry) => true
});

const companyEntry = toRef(props, "companyEntry");

const { t } = useI18n();
const toaster = useToaster();

const resumeCompanyEntriesService = ResumesCompanyEntriesServiceInstance;

const autoSubmitStop = ref(true);
const valid = ref<boolean>(true);
const formCompany = ref<string | null>(null);
const formWebSiteUrl = ref<string | null>(null);
const formDescription = ref<string | null>(null);

const createMode = computed(() => companyEntry.value?.id === 0);

onMounted(() => {
  const company = companyEntry.value!;
  formCompany.value = company.company;
  formWebSiteUrl.value = company.webSiteUrl;
  formDescription.value = company.description;
});

function removeCompany(item: TResumeCompanyEntry) {
  const companies = companyEntries.value;
  const itemIndex = companies.indexOf(item);
  if (itemIndex > -1) {
    companies.splice(itemIndex, 1);
  }
}

function submit() {
  try {
    if (createMode.value) {
      resumeCompanyEntriesService.createResumeCompanyEntry({
        resumeId: companyEntry.value?.resumeId,
        company: formCompany.value!,
        webSiteUrl: formWebSiteUrl.value,
        description: formDescription.value
      });
    }
    else {
      resumeCompanyEntriesService.updateResumeCompanyEntry({
        id: companyEntry.value?.id,
        company: formCompany.value!,
        webSiteUrl: formWebSiteUrl.value,
        description: formDescription.value
      });
    }
  }
  catch (error) {
    toaster.error("Произошла ошибка при сохранении компании");
  }
}

function onFormFocus() {
  autoSubmitStop.value = true;
}

function onFormBlur() {
  autoSubmitStop.value = false;
}

</script>

<style scoped src="./ResumeEditCompanyEntry.scss" />
