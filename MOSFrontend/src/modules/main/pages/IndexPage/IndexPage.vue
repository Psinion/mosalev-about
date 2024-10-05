<template>
  <FlatLayout>
    <div class="index-page">
      <h2>Добро пожаловать!</h2>
      <div class="welcome-text body-regular">
        <p>
          Этот сайт был создан развлечения ради, чтобы разобраться с контейнеризацией и развёртыванием проекта на удалённом сервере.
          Плюс хотелось зафиксировать базис клиент-серверной монолитной архитектуры, которую я смогу использовать в дальнейшем.
          Здесь вы найдёте некоторую информацию обо мне и моих проектах.
          Исходный код <strong>этого</strong> проекта доступен на <a href="https://github.com/Psinion/mosalev-about">GitHub</a>.
        </p>
      </div>
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">

import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
</script>

<style scoped src="./IndexPage.scss"></style>
