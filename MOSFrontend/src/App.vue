<template>
  <div class="background">
    <RouterView />
  </div>
</template>

<script setup lang="ts">
import { RouterView } from "vue-router";
</script>

<style scoped lang="scss">
.background {
  background: var(--background-body);
}
</style>
