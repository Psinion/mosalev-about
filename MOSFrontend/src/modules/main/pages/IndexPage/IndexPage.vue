<template>
  <FlatLayout align-horizontal="center">
    <div class="index-page">
      <RouterView />
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">
import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
</script>

<style src="./IndexPage.scss" />
