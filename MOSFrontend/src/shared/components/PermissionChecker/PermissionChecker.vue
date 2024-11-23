<template>
  <slot v-if="visiblePermission" />
</template>

<script setup lang="ts">
import { computed } from "vue";
import { useUserStore } from "@/shared/stores/userStore.ts";

const userStore = useUserStore();

const visiblePermission = computed(() => userStore.user != null);
</script>
