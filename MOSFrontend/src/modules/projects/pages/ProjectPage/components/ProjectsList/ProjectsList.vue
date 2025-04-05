<template>
  <article class="projects-list">
    <div class="actions" />
    <div class="content">
      <h2>{{ t('pages.projectsList') }}</h2>
      <div
        class="projects"
      />
    </div>
  </article>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";

const { t } = useI18n();
</script>

<style scoped src="./ProjectsList.scss" />
