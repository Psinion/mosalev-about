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
          v-model="form.title"
          label="Название"
          required
        />

        <div class="fio-input">
          <PsiInput
            v-model="form.lastName"
            label="Фамилия"
            required
          />
          <PsiInput
            v-model="form.firstName"
            label="Имя"
            required
          />
        </div>

        <PsiInput
          v-model="form.email"
          label="Email"
          required
        />
        <div class="salary-input">
          <PsiInputNumeric
            v-model="form.salary"
            label="Зарплата"
            :min="0"
          />
          <PsiToggle
            v-model="form.currencyType"
            inactive-label="₽"
            active-label="$"
          />
        </div>
        <PsiTextarea
          v-model="form.about"
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

type TForm = {
  title?: string;
  firstName?: string;
  lastName?: string;
  email?: string;
  salary?: number;
  currencyType: boolean;
  about?: string | null;
};
const form = ref<TForm>({
  currencyType: false
});

onMounted(async () => refresh());

async function refresh() {
  if (!currentResume.value) {
    return;
  }

  const resume = currentResume.value;
  const fm = form.value;

  fm.title = resume.title;
  fm.firstName = resume.firstName;
  fm.lastName = resume.lastName;
  fm.email = resume.email;
  fm.salary = resume.salary;
  fm.currencyType = resume.currencyType === CurrencyType.Ruble;
  fm.about = resume.about;
}

async function onSave() {
  try {
    loading.value = true;
    const fm = form.value;

    if (createMode.value) {
      const resumeToSave: TCreateResumeRequest = {
        title: fm.title!,
        firstName: fm.firstName!,
        lastName: fm.lastName!,
        email: fm.email!,
        salary: fm.salary ?? 0,
        currencyType: fm.currencyType ? CurrencyType.Ruble : CurrencyType.Dollar,
        about: fm.about ?? null
      };

      await resumeStore.createResume(resumeToSave);
      toaster.success(t("resume.edit.toasterResumeCreateHeader"));
    }
    else {
      const resumeToSave: TUpdateResumeRequest = {
        id: currentResume.value!.id!,
        title: fm.title!,
        firstName: fm.firstName!,
        lastName: fm.lastName!,
        email: fm.email!,
        salary: fm.salary ?? 0,
        currencyType: fm.currencyType ? CurrencyType.Ruble : CurrencyType.Dollar,
        about: fm.about ?? null
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
