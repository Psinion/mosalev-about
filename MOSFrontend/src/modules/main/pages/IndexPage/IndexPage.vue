<template>
  <FlatLayout align-horizontal="center">
    <div class="index-page">
      <div class="actions">
        <PsiButton>Редактировать</PsiButton>
      </div>
      <h2>Добро пожаловать!</h2>
      <div class="welcome-text body-regular">
        <div>
          <h3>О сайте</h3>
          <p>
            Этот сайт был создан развлечения ради, чтобы разобраться с контейнеризацией и развёртыванием проекта на удалённом сервере.
            Плюс хотелось зафиксировать базис клиент-серверной монолитной архитектуры, которую я смогу использовать в дальнейшем.
            Здесь вы найдёте некоторую информацию обо мне и моих проектах.
            Исходный код <strong>этого</strong> проекта доступен на <a href="https://github.com/Psinion/mosalev-about">GitHub</a>.
          </p>
          <p>
            Какие-либо pet-проекты у меня только gamedev характера и направлены чисто для себя, показывать их желания пока нет.
            Поэтому возникла идея создать этот сайт. Он простой и его статический аналог можно было бы легко создать в блокнотике,
            но я преследовал цель некой демонстрации своих знаний, чем желание создать практичный продукт.
          </p>
        </div>
      </div>
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">
import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
import PsiButton from "@/shared/components/PsiButton/PsiButton.vue";
</script>

<style src="./IndexPage.scss" />
