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
          <input>
        </div>
        <div class="input-block">
          <label>Пароль</label>
          <input type="password">
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
import UsersServiceInstance from "@/shared/services/UsersService.ts";

async function onSubmit() {
  try {
    await UsersServiceInstance.authenticate({
      userName: "kek",
      password: "kek"
    });
  }
  catch (error) {
    console.log(error);
  }
}
</script>

<style scoped src="./LoginPage.scss" />
