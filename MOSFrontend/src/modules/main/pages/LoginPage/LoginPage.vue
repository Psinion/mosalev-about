<template>
  <FlatLayout class="login-page-layout">
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
import { reactive } from "vue";

const loginForm = reactive({
    login: "",
    password: ""
});

function onSubmit() {
    console.log("submit!");
};
</script>

<style src="./LoginPage.scss" />
