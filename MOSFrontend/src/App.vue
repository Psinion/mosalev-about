<template>
  <div>
    <RouterView />
  </div>
</template>

<script setup lang="ts">
import { RouterView } from "vue-router";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { onMounted } from "vue";
import { useToaster } from "@/shared/utils/toaster.ts";
import { useI18n } from "vue-i18n";
import { ServerError } from "@/shared/utils/requests/errorHandlers.ts";

const toaster = useToaster();
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
    }
    toaster.error(t("toaster.commonErrorHeader"), error);
  }
});

</script>
