<template>
  <section
    class="layout-header-user"
  >
    <h3 class="secondary-up">
      {{ username }}
    </h3>
  </section>
</template>

<script setup lang="ts">
import { useUserStore } from "@/shared/stores/userStore.ts";
import { computed } from "vue";

const userStore = useUserStore();

const user = computed(() => userStore.user);
const username = computed(() => user.value?.userName);

</script>

<style scoped src="./LayoutHeaderUser.scss" />
