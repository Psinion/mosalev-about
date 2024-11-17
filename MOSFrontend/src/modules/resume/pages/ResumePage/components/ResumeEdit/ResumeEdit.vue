<template>
  <article class="resume-edit">
    <div class="content">
      <h2>Создание резюме</h2>
      <div class="actions" />
      <form @submit.prevent="onSave">
        <PsiInput
          v-model="title"
          label="Название"
        />
        <PsiInput
          v-model="email"
          label="Email"
        />
        <div class="salary-input">
          <PsiInputNumeric
            v-model="salary"
            label="Зарплата"
            :min="0"
          />
          <PsiToggle
            v-model="currencyType"
            inactive-label="₽"
            active-label="$"
          />
        </div>
        <div class="actions">
          <PsiButton type="submit">
            Сохранить
          </PsiButton>
        </div>
      </form>
    </div>
  </article>
</template>

<script setup lang="ts">
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { ref } from "vue";
import { TCreateResumeRequest } from "@/shared/services/base";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiToggle from "@/shared/PsiUI/components/PsiToggle/PsiToggle.vue";
import PsiInputNumeric from "@/shared/PsiUI/components/PsiInputNumeric/PsiInputNumeric.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";

const resumesService = ResumesServiceInstance;

const title = ref<string | null>();
const email = ref<string | null>();
const salary = ref<number | null>();
const currencyType = ref<boolean>(false);

async function onSave() {
  const resumeToSave: TCreateResumeRequest = {
    title: title.value,
    email: email.value,
    salary: salary.value,
    currencyType: currencyType.value ? 0 : 1
  };

  await resumesService.createResume(resumeToSave);
}
</script>

<style src="./ResumeEdit.scss" />
