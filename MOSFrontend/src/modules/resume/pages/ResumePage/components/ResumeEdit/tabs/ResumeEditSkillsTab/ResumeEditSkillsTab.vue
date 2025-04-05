<template>
  <article class="resume-edit-skills-tab">
    <div class="content">
      <h3>Навыки</h3>
      <section class="skills">
        <div
          v-for="(companyEntry, index) in skillsList"
          :key="companyEntry.id"
          class="skill-block"
        >
          <ResumeEditSkill
            v-model="skillsList[index]"
            @delete="deleteSkill"
          />
        </div>
      </section>
      <div class="company-actions">
        <PsiButton @click="addSkill">
          Добавить компанию
        </PsiButton>
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { onMounted, PropType, ref, toRef } from "vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { IResumeSkill, ResumeSkillLevelType, TResume, TResumeCompanyEntry } from "@/shared/types/resume.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import ResumeCompanyEntriesServiceInstance from "@/shared/services/ResumeCompanyEntriesService.ts";
import ResumeEditSkill
  from "@/modules/resume/pages/ResumePage/components/ResumeEdit/components/ResumeEditSkill/ResumeEditSkill.vue";

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

const currentResume = toRef(props, "resume");

const skillsList = ref<IResumeSkill[]>([]);

onMounted(async () => refresh());

async function refresh() {
  if (!currentResume.value) {
    return;
  }

  const resume = currentResume.value;
  skillsList.value = resume.skills;
}

async function deleteSkill(item: IResumeSkill) {
  const companies = skillsList.value;
  const itemIndex = companies.indexOf(item);
  if (itemIndex > -1) {
    companies.splice(itemIndex, 1);
  }
}

function addSkill() {
  const skills = skillsList.value;
  skills.push({
    id: 0,
    resumeId: currentResume.value!.id,
    name: undefined,
    level: ResumeSkillLevelType.Low
  });
}
</script>

<style scoped src="./ResumeEditSkillsTab.scss" />
