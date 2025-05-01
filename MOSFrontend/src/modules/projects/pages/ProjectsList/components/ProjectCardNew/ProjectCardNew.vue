<template>
  <section class="project-card-new">
    <h3>{{ t('projects.list.newProjectTitle') }}</h3>
    <div class="caption-regular">
      {{ t('projects.list.newProjectDescription') }}
    </div>
  </section>
</template>

<script setup lang="ts">
import { useI18n } from "vue-i18n";

const { t } = useI18n();

</script>

<style scoped src="./ProjectCardNew.scss" />
