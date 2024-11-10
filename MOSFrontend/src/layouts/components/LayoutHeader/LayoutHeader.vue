<template>
  <section class="layout-header">
    <div class="content">
      <RouterLink
        class="logo"
        :to="indexRoute"
      >
        <img
          src="@/assets/images/logo.svg"
          alt="logo"
        >
        <h1>Mosalev</h1>
      </RouterLink>
      <div class="actions">
        <LayoutHeaderActions
          title="Обо мне"
          :route="aboutRoute"
        />
        <LayoutHeaderActions
          title="Проекты"
          disabled
        />
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import LayoutHeaderActions
  from "@/layouts/components/LayoutHeader/components/LayoutHeaderButton/LayoutHeaderButton.vue";
import { computed } from "vue";
import { RouteNames } from "@/router/routeNames.ts";

const indexRoute = computed(() => {
  return {
    name: RouteNames.IndexView
  };
});
const aboutRoute = computed(() => {
  return {
    name: RouteNames.AboutView
  };
});
</script>

<style scoped src="./LayoutHeader.scss" />
