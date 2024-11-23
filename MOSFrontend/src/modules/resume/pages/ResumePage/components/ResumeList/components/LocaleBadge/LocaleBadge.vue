<template>
  <section
    class="locale-badge h4"
    :class="localeClass"
  >
    {{ localeText }}
  </section>
</template>

<script setup lang="ts">

import { computed, PropType } from "vue";
import { AppLocale } from "@/shared/enums/common.ts";

const props = defineProps({
  locale: {
    type: Number as PropType<AppLocale>,
    required: true
  }
});

const localeText = computed(() => props.locale === AppLocale.ru ? "ru" : "en");
const localeClass = computed(() => props.locale === AppLocale.ru ? "ru" : "en");

</script>

<style scoped src="./LocaleBadge.scss" />
