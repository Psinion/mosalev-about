<template>
  <article class="resume-edit-companies-list">
    <h3>Опыт работы</h3>
    <section class="companies">
      <div
        v-for="companyEntry in modelValue"
        :key="companyEntry.id"
        class="company-block"
      >
        <section class="company">
          <div class="company-input">
            <PsiInput
              v-model="companyEntry.company"
              label="Организация"
            />
            <PsiButton
              flat
              icon="close"
              @click="removeCompany(companyEntry)"
            />
          </div>
          <PsiInput
            v-model="companyEntry.webSiteUrl"
            label="Сайт"
          />
          <PsiTextarea
            v-model="companyEntry.description"
            label="Описание"
            resizable="vertical"
          />
        </section>
        <ResumeEditPostsList
          v-model="companyEntry.resumePosts"
          class="posts-list"
        />
      </div>
    </section>
    <div class="company-actions">
      <PsiButton @click="addCompany">
        Добавить компанию
      </PsiButton>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { PropType, toRef } from "vue";
import { TResumeCompanyEntry } from "@/shared/types/resume.ts";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import ResumeEditPostsList
  from "@/modules/resume/pages/ResumePage/components/ResumeEdit/components/ResumeEditPostsList/ResumeEditPostsList.vue";

const props = defineProps({
  modelValue: {
    type: Array as PropType<TResumeCompanyEntry[]>,
    default: () => []
  }
});

const emit = defineEmits({
  "update:modelValue": (value: TResumeCompanyEntry[]) => true
});

const companyEntries = toRef(props, "modelValue");

const { t } = useI18n();
const toaster = useToaster();

function addCompany() {
  const companies = companyEntries.value;
  companies.push({
    id: 0,
    resumeId: 0,
    company: "",
    description: null,
    webSiteUrl: null,
    resumePosts: [{
      id: 0,
      companyId: 0,
      name: ""
    }]
  });
  emit("update:modelValue", companies);
}

function removeCompany(item: TResumeCompanyEntry) {
  const companies = companyEntries.value;
  const itemIndex = companies.indexOf(item);
  if (itemIndex > -1) {
    companies.splice(itemIndex, 1);
    emit("update:modelValue", companies);
  }
}
</script>

<style scoped src="./ResumeEditCompaniesList.scss" />
