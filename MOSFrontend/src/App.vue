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

const toaster = useToaster();
const userStore = useUserStore();
const { t } = useI18n();

onMounted(async () => {
  try {
    await userStore.checkLogin();
  }
  catch (error) {
    toaster.error(t("auth.toasterTokenExpiredHeader"), t("auth.toasterTokenExpiredDescription"));
  }
});

</script>
