<template>
  <FlatLayout
    align-horizontal="center"
    align-vertical="center"
  >
    <div
      class="login-page"
      @submit.prevent="onSubmit"
    >
      <form class="login-form">
        <div class="input-block">
          <label>Логин</label>
          <input v-model="userName">
        </div>
        <div class="input-block">
          <label>Пароль</label>
          <input
            v-model="password"
            type="password"
          >
        </div>
        <button class="submit-button">
          Войти
        </button>
      </form>
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">
import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
import { ref } from "vue";
import { useUserStore } from "@/shared/stores/userStore.ts";

const userStore = useUserStore();

const userName = ref <string | null> (null);
const password = ref <string | null> (null);

async function onSubmit() {
  if (!userName.value || !password.value) {
    return;
  }

  try {
    await userStore.login(userName.value, password.value);
  }
  catch (error) {
    console.log(error);
  }
}
</script>

<style scoped src="./LoginPage.scss" />
