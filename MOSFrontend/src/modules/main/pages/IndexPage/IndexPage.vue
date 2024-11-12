<template>
  <FlatLayout align-horizontal="center">
    <div class="index-page">
      <h2 class="font-color">
        {{ t('index.welcome') }}
      </h2>
      <div class="welcome-text typography-block body-regular">
        <div>
          <h3>{{ t('index.aboutWebsiteHeader') }}</h3>
          <p>
            {{ t('index.aboutWebsiteContent1') }} <strong>{{ t('index.aboutWebsiteContent2') }}</strong> {{ t('index.aboutWebsiteContent3') }} <a href="https://github.com/Psinion/mosalev-about">GitHub</a>.
          </p>
          <p>
            {{ t('index.aboutWebsiteContent4') }}
          </p>
        </div>
      </div>
    </div>
  </FlatLayout>
</template>

<script setup lang="ts">
import FlatLayout from "@/layouts/FlatLayout/FlatLayout.vue";
import { useI18n } from "vue-i18n";

const { t } = useI18n();

</script>

<style src="./IndexPage.scss" />
