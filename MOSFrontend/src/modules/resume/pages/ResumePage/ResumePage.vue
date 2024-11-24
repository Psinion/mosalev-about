<template>
  <ContentLayout>
    <div class="resume-page">
      <RouterView />
    </div>
  </ContentLayout>
</template>

<script setup lang="ts">
import ContentLayout from "@/layouts/ContentLayout/ContentLayout.vue";
</script>

<style scoped src="./ResumePage.scss" />
