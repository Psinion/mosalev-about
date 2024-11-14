<template>
  <div>
    <RouterView />
  </div>
</template>

<script setup lang="ts">
import { RouterView } from "vue-router";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { onMounted } from "vue";

const userStore = useUserStore();

onMounted(() => {
  try {
    userStore.checkLogin();
  }
  catch (error) {
    console.log(error);
  }
});

</script>
