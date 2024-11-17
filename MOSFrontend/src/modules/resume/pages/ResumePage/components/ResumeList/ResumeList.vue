<template>
  <article class="resume-list">
    <div class="actions">
      <PsiButton :route="resumeEditRoute">
        Создать
      </PsiButton>
    </div>
    <div class="text body-regular">
      <div>
        <h2>Список резюме</h2>
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";
import PsiButton from "@/shared/components/PsiButton/PsiButton.vue";
import { computed } from "vue";
import { RouteNames } from "@/router/routeNames.ts";

const { t } = useI18n();

const resumeEditRoute = computed(() => {
  return {
    name: RouteNames.ResumeEdit
  };
});

</script>

<style src="./ResumeList.scss" />
