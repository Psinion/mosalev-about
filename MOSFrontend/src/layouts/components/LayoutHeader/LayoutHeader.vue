<template>
  <section class="layout-header">
    <div class="content">
      <div class="left-panel">
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
      </div>
      <div class="right-panel">
        <div class="center-panel">
          <div class="map">
            <LayoutHeaderActions
              :title="t('layoutHeader.projects')"
              disabled
            />
          </div>
        </div>
        <div class="admin-panel">
          <LayoutHeaderUser
            v-if="authorised"
            class="user"
          />
          <PsiToggle
            v-model="isRuLanguage"
            class="language-toggle"
            inactive-label="en"
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
import { useI18n } from "vue-i18n";
import PsiToggle from "@/shared/PsiUI/components/PsiToggle/PsiToggle.vue";
import { AppLocale } from "@/shared/enums/common.ts";

const userStore = useUserStore();

const { t } = useI18n();

const isRuLanguage = ref(userStore.locale === AppLocale.ru);
watch(() => isRuLanguage.value, (value) => {
  userStore.setLocale(value ? AppLocale.ru : AppLocale.en);
});

const authorised = computed(() => userStore.user !== null);

const indexRoute = computed(() => {
  return {
    name: RouteNames.Index
  };
});
</script>

<style scoped src="./LayoutHeader.scss" />
