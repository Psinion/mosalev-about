<template>
  <FlatLayout
    align-horizontal="center"
    align-vertical="center"
  >
    <div
      class="login-page"
      @submit.prevent="onSubmit"
    >
      <form class="login-form">
        <PsiInput
          v-model="userName"
          :label="t('login.formLoginLabel')"
          required
        />
        <PsiInput
          v-model="password"
          :label="t('login.formPasswordLabel')"
          type="password"
          required
        />
        <PsiButton
          type="submit"
        >
          {{ t('login.formSubmitButton') }}
        </PsiButton>
      </form>
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">
import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
import { ref } from "vue";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import { useForm, useIsFormValid } from "vee-validate";

const toaster = useToaster();
const { t } = useI18n();
const userStore = useUserStore();

const form = useForm();
const isValid = useIsFormValid();

const userName = ref <string | null> (null);
const password = ref <string | null> (null);

async function onSubmit() {
  await form.validate();
  if (!isValid.value) {
    return;
  }

  try {
    await userStore.login(userName.value, password.value);
  }
  catch (error) {
    if (error instanceof ServerError) {
      if (error.code === "incorrect_data") {
        toaster.error(t("login.toasterIncorrectDataHeader"));
      }
      else {
        toaster.error(t("toaster.commonErrorHeader"), error.message);
      }
    }
  }
}
</script>

<style scoped src="./LoginPage.scss" />
