<template>
  <div>
    <RouterView />
  </div>
</template>

<script setup lang="ts">
import { RouterView, useRoute } from "vue-router";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { onMounted, watch } from "vue";
import { useToaster } from "@/shared/PsiUI/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import { setPageTitle } from "@/shared/utils/helpers/helpers.ts";
import { ServerError } from "@/shared/utils/requests/errors.ts";

const toaster = useToaster();
const route = useRoute();
const userStore = useUserStore();
const { t } = useI18n();

onMounted(async () => {
  try {
    if (userStore.token) {
      await userStore.checkLogin();
    }
  }
  catch (error) {
    if (error instanceof ServerError) {
      if (error.statusCode) {
        toaster.error(t("auth.toasterTokenExpiredHeader"), t("auth.toasterTokenExpiredDescription"));
        await userStore.logout();
        return;
      }
      toaster.error(t("toaster.commonErrorHeader"), error.message);
    }
  }
});

watch(route, (value) => {
  const titleCode = value.meta["titleCode"] as string;
  if (titleCode) {
    setPageTitle(t(titleCode));
    return;
  }

  setPageTitle("Mosalev");
});

</script>
