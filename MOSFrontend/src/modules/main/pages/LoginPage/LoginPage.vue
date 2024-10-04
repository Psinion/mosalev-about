<template>
  <FlatLayout>
    <div class="login-page">
      <el-form
        :model="loginForm"
        class="login-form"
        label-position="top"
      >
        <el-form-item
          label="Логин"
          required
        >
          <el-input v-model="loginForm.login" />
        </el-form-item>

        <el-form-item
          label="Пароль"
          required
        >
          <el-input
            v-model="loginForm.password"
            type="password"
            show-password
          />
        </el-form-item>

        <el-form-item>
          <el-button
            type="primary"
            @click="onSubmit"
          >
            Create
          </el-button>
        </el-form-item>
      </el-form>
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

<style scoped src="./LoginPage.scss" />
