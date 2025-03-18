<template>
  <article class="resume-edit-skills">
    <PsiForm
      v-model:auto-submit-timer-stop="autoSubmitStop"
      class="form"
      :auto-submit-timer="2000"
      @valid="valid = $event"
      @submit="submit"
    >
      <div class="skill-input">
        <PsiInput
          v-model="form.company"
          required
          :max-length="80"
          @focus="onFormFocus"
          @blur="onFormBlur"
        />
        <PsiSlider
          :max="2"
        />
        <PsiButton
          flat
          icon="close"
          @click="removeCompany"
        />
      </div>
    </PsiForm>
  </article>
</template>

<script setup lang="ts">
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import { computed, onMounted, PropType, ref, toRef } from "vue";
import { TResumeCompanyEntry } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import ResumesCompanyEntriesServiceInstance from "@/shared/services/ResumeCompanyEntriesService.ts";
import PsiSlider from "@/shared/PsiUI/components/PsiSlider/PsiSlider.vue";

const props = defineProps({
  modelValue: {
    type: Object as PropType<TResumeCompanyEntry>,
    required: true
  }
});

const emit = defineEmits({
  "update:modelValue": (value: TResumeCompanyEntry) => true,
  "delete": (value: TResumeCompanyEntry) => true
});

const companyEntry = toRef(props, "modelValue");

const { t } = useI18n();
const toaster = useToaster();

const resumeCompanyEntriesService = ResumesCompanyEntriesServiceInstance;

const autoSubmitStop = ref(true);
const valid = ref<boolean>(true);

type TForm = {
  company?: string;
  webSiteUrl?: string;
  description?: string;
};
const form = ref<TForm>({});

const createMode = computed(() => companyEntry.value?.id === 0);

onMounted(() => {
  const company = companyEntry.value!;
  const fm = form.value;
  fm.company = company.company;
  fm.webSiteUrl = company.webSiteUrl ?? undefined;
  fm.description = company.description ?? undefined;
});

function removeCompany() {
  emit("delete", companyEntry.value);
}

async function submit() {
  try {
    const fm = form.value;
    if (createMode.value) {
      const savedCompany = await resumeCompanyEntriesService.createResumeCompanyEntry({
        resumeId: companyEntry.value?.resumeId,
        company: fm.company!,
        webSiteUrl: fm.webSiteUrl,
        description: fm.description
      });
      emit("update:modelValue", savedCompany);
    }
    else {
      const savedCompany = await resumeCompanyEntriesService.updateResumeCompanyEntry({
        id: companyEntry.value?.id,
        company: fm.company!,
        webSiteUrl: fm.webSiteUrl,
        description: fm.description
      });

      const company = companyEntry.value;
      company.company = savedCompany.company;
      company.webSiteUrl = savedCompany.webSiteUrl;
      company.description = savedCompany.description;
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

<style scoped src="./ResumeEditSkill.scss" />
