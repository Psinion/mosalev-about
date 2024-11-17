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
              :title="t('layoutHeader.aboutMe')"
              :route="aboutRoute"
            />
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
import { computed, onMounted, ref, watch } from "vue";
import { RouteNames } from "@/router/routeNames.ts";
import { useUserStore } from "@/shared/stores/userStore.ts";
import LayoutHeaderUser from "@/layouts/components/LayoutHeader/components/LayoutHeaderUser/LayoutHeaderUser.vue";
import PsiToggle from "@/shared/components/PsiToggle/PsiToggle.vue";
import { useI18n } from "vue-i18n";
import { useRoute } from "vue-router";
import { setPageTitle } from "@/shared/utils/helpers.ts";

const MOS_LOCALE_KEY = "locale";

const userStore = useUserStore();
const route = useRoute();

const { locale, t } = useI18n();

const isRuLanguage = ref(locale.value === "ru");
watch(() => isRuLanguage.value, (value) => {
  const localeCode = value ? "ru" : "en";
  locale.value = localeCode;
  localStorage.setItem(MOS_LOCALE_KEY, localeCode);

  const titleCode = route.meta["titleCode"] as string;
  setPageTitle(t(titleCode));
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

onMounted(() => {
  const localeCode = localStorage.getItem(MOS_LOCALE_KEY);
  if (localeCode) {
    locale.value = localeCode;
    isRuLanguage.value = locale.value === "ru";
  }
});
</script>

<style scoped src="./LayoutHeader.scss" />
