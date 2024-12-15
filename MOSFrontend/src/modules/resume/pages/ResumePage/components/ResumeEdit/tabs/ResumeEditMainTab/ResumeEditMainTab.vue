<template>
  <article class="resume-edit-main-tab">
    <div class="content">
      <PsiForm
        v-if="!loading"
        v-slot="{ valid }"
        @submit="onSave"
      >
        <h3>Профиль</h3>
        <PsiInput
          v-model="title"
          label="Название"
          required
        />

        <div class="fio-input">
          <PsiInput
            v-model="lastName"
            label="Фамилия"
            required
          />
          <PsiInput
            v-model="firstName"
            label="Имя"
            required
          />
        </div>

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
          />
          <PsiToggle
            v-model="currencyType"
            inactive-label="₽"
            active-label="$"
          />
        </div>
        <PsiTextarea
          v-model="about"
          label="О себе"
          :rows="7"
          resizable="vertical"
        />

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
import { onMounted, PropType, ref, toRef } from "vue";
import { TCreateResumeRequest, TUpdateResumeRequest } from "@/shared/services/base";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiToggle from "@/shared/PsiUI/components/PsiToggle/PsiToggle.vue";
import PsiInputNumeric from "@/shared/PsiUI/components/PsiInputNumeric/PsiInputNumeric.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";
import { CurrencyType, TResume } from "@/shared/types/resume.ts";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import PsiTextarea from "@/shared/PsiUI/components/PsiTextarea/PsiTextarea.vue";
import { useResumeStore } from "@/modules/resume/stores/resumeStore.ts";

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
const resumeStore = useResumeStore();

const loading = ref(false);

const currentResume = toRef(props, "resume");
const createMode = toRef(props, "createMode");

const title = ref<string | null>();
const firstName = ref<string | null>();
const lastName = ref<string | null>();
const email = ref<string | null>();
const salary = ref<number | null>();
const currencyType = ref<boolean>(false);
const about = ref<string | null>();

onMounted(async () => refresh());

async function refresh() {
  if (!currentResume.value) {
    return;
  }

  const resume = currentResume.value;

  title.value = resume.title;
  firstName.value = resume.firstName;
  lastName.value = resume.lastName;
  email.value = resume.email;
  salary.value = resume.salary;
  currencyType.value = resume.currencyType === CurrencyType.Ruble;
  about.value = resume.about;
}

async function onSave() {
  try {
    loading.value = true;
    if (createMode.value) {
      const resumeToSave: TCreateResumeRequest = {
        title: title.value!,
        firstName: firstName.value!,
        lastName: lastName.value!,
        email: email.value!,
        salary: salary.value ?? 0,
        currencyType: currencyType.value ? CurrencyType.Ruble : CurrencyType.Dollar,
        about: about.value ?? null
      };

      await resumeStore.createResume(resumeToSave);
      toaster.success(t("resume.edit.toasterResumeCreateHeader"));
    }
    else {
      const resumeToSave: TUpdateResumeRequest = {
        id: currentResume.value!.id!,
        title: title.value!,
        firstName: firstName.value!,
        lastName: lastName.value!,
        email: email.value!,
        salary: salary.value ?? 0,
        currencyType: currencyType.value ? CurrencyType.Ruble : CurrencyType.Dollar,
        about: about.value ?? null
      };

      await resumeStore.updateResume(resumeToSave);
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
</script>

<style scoped src="./ResumeEditMainTab.scss" />
