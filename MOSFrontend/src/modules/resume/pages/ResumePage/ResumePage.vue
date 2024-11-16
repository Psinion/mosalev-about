<template>
  <FlatLayout align-horizontal="center">
    <div class="resume-page">
      <RouterView />
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">
import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
</script>

<style src="./ResumePage.scss" />
