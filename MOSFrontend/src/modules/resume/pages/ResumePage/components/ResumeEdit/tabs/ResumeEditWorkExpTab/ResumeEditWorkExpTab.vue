<template>
  <article class="resume-edit-work-exp-tab">
    <div class="content">
      <h3>Опыт работы</h3>
      <section class="companies">
        <div
          v-for="companyEntry in companyEntries"
          :key="companyEntry.id"
          class="company-block"
        >
          <ResumeEditCompanyEntry :company-entry="companyEntry" />
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
    </div>
  </article>
</template>

<script setup lang="ts">
import { onMounted, PropType, ref, toRef } from "vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { TResume, TResumeCompanyEntry } from "@/shared/types/resume.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import ResumeEditPostsList
  from "@/modules/resume/pages/ResumePage/components/ResumeEdit/components/ResumeEditPostsList/ResumeEditPostsList.vue";
import ResumeEditCompanyEntry
  from "@/modules/resume/pages/ResumePage/components/ResumeEdit/components/ResumeEditCompanyEntry/ResumeEditCompanyEntry.vue";

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

const loading = ref(false);

const currentResume = toRef(props, "resume");
const createMode = toRef(props, "createMode");

const companyEntries = ref<TResumeCompanyEntry[]>([]);

onMounted(async () => refresh());

async function refresh() {
  if (!currentResume.value) {
    return;
  }

  const resume = currentResume.value;
}

async function onSave() {
  try {
    loading.value = true;
    if (createMode.value) {
      /* const resumeToSave: TCreateResumeRequest = {
        title: title.value!,
        firstName: firstName.value!,
        lastName: lastName.value!,
        email: email.value!,
        salary: salary.value ?? 0,
        currencyType: currencyType.value ? CurrencyType.Ruble : CurrencyType.Dollar,
        about: about.value ?? null
      };

      await resumesService.createResume(resumeToSave); */
      toaster.success(t("resume.edit.toasterResumeCreateHeader"));
    }
    else {
      /* const resumeToSave: TUpdateResumeRequest = {
        id: currentResume.value!.id!,
        title: title.value!,
        firstName: firstName.value!,
        lastName: lastName.value!,
        email: email.value!,
        salary: salary.value ?? 0,
        currencyType: currencyType.value ? CurrencyType.Ruble : CurrencyType.Dollar,
        about: about.value ?? null
      };

      await resumesService.updateResume(resumeToSave); */
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
}
</script>

<style scoped src="./ResumeEditWorkExpTab.scss" />
