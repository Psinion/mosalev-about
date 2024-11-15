<template>
  <BaseError
    :status-code="404"
    :header="t('errors.error404header')"
  />
</template>

<script setup lang="ts">
import BaseError from "@/modules/errors/components/BaseError/BaseError.vue";
import { useI18n } from "vue-i18n";

const { t } = useI18n();
</script>
