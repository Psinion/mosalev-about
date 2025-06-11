<template>
  <FlatLayout
    align-horizontal="center"
    align-vertical="center"
  >
    <div
      class="login-page"
    >
      <PsiForm
        v-slot="{valid}"
        class="login-form"
        @submit="onSubmit"
      >
        <PsiInput
          v-model.trim="form.userName"
          :label="t('login.formLoginLabel')"
          required
        />
        <PsiInput
          v-model="form.password"
          :label="t('login.formPasswordLabel')"
          password
          required
        />
        <PsiButton
          native-type="submit"
          :disabled="!valid || loading"
        >
          {{ t('login.formSubmitButton') }}
        </PsiButton>
      </PsiForm>
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">
import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
import { ref } from "vue";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import { ServerError } from "@/shared/utils/requests/errors.ts";
import PsiInput from "@/shared/PsiUI/components/PsiInput/PsiInput.vue";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import PsiForm from "@/shared/PsiUI/components/PsiForm/PsiForm.vue";

const toaster = useToaster();
const { t } = useI18n();
const userStore = useUserStore();

const loading = ref(false);

type TForm = {
  userName?: string;
  password?: string;
};
const form = ref<TForm>({});

async function onSubmit() {
  try {
    loading.value = true;
    await userStore.login(form.value.userName!, form.value.password!);
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
  finally {
    loading.value = false;
  }
}
</script>

<style scoped src="./LoginPage.scss" />
