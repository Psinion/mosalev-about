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
          <PsiInput
            v-model="salary"
            label="Зарплата"
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
import PsiButton from "@/shared/components/PsiButton/PsiButton.vue";
import PsiInput from "@/shared/components/PsiInput/PsiInput.vue";
import PsiToggle from "@/shared/components/PsiToggle/PsiToggle.vue";
import ResumesServiceInstance from "@/shared/services/ResumesService.ts";
import { ref } from "vue";
import { TCreateResumeRequest } from "@/shared/services/base/IResumesService.ts";

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
