<template>
  <article class="resume-edit-company-entry">
    <PsiForm
      ref="psiFormRef"
      class="form"
      @valid="valid = $event"
    >
      <div class="company-input">
        <PsiInput
          v-model="formCompany"
          label="Организация"
          required
          @blur="onUpdate"
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
        @blur="onUpdate"
      />
      <PsiTextarea
        v-model="formDescription"
        label="Описание"
        resizable="vertical"
        @blur="onUpdate"
      />
    </PsiForm>
  </article>
</template>

<script setup lang="ts">
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { onMounted, PropType, ref, toRef } from "vue";
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

const psiFormRef = ref();

const valid = ref<boolean>(true);
const formCompany = ref<string | null>(null);
const formWebSiteUrl = ref<string | null>(null);
const formDescription = ref<string | null>(null);

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

function onUpdate() {
  if (psiFormRef.value?.validate()) {
    console.log(valid.value);
  }
}

</script>

<style scoped src="./ResumeEditCompanyEntry.scss" />
