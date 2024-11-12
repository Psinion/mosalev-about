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
      <div class="right-panel">
        <div class="center-panel">
          <LayoutHeaderUser
            v-if="authorised"
            class="user"
          />
          <div class="map">
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
        <div class="language">
          <PsiToggle
            v-model="isRuLanguage"
            class="language-toggle"
            inactive-label="eng"
            active-label="ru"
          />
        </div>
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">
import LayoutHeaderActions
  from "@/layouts/components/LayoutHeader/components/LayoutHeaderButton/LayoutHeaderButton.vue";
import { computed, ref, watch } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import { useUserStore } from "@/shared/stores/userStore.ts";
import LayoutHeaderUser from "@/layouts/components/LayoutHeader/components/LayoutHeaderUser/LayoutHeaderUser.vue";
import PsiToggle from "@/shared/components/PsiToggle/PsiToggle.vue";
import { useI18n } from "vue-i18n";

const userStore = useUserStore();

const { locale } = useI18n();

const isRuLanguage = ref(locale.value === "ru");
watch(() => isRuLanguage.value, (value) => {
  locale.value = value ? "ru" : "en";
});

const authorised = computed(() => userStore.user !== null);

const indexRoute = computed(() => {
  return {
    name: RouteNames.Index
  };
});
const aboutRoute = computed(() => {
  return {
    name: RouteNames.AboutView
  };
});
</script>

<style scoped src="./LayoutHeader.scss" />
