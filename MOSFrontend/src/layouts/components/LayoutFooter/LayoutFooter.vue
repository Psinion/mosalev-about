<template>
  <section class="layout-footer">
    <div class="content hint-regular">
      <div>© {{ copyRightYears }} Mosalev Daniel</div>
      <div class="links">
        <FooterLink
          icon="telegram"
          title="Telegram"
          href="https://t.me/mosalev_daniel"
        />
        <FooterLink
          icon="github"
          title="GitHub"
          href="https://github.com/Psinion"
        />
        <FooterLink
          icon="steam"
          title="Steam"
          href="https://steamcommunity.com/id/psinion/"
        />
      </div>
    </div>
  </section>
</template>

<script setup lang="ts">

import { computed } from "vue";
import FooterLink from "@/layouts/components/LayoutFooter/components/FooterLink/FooterLink.vue";

const copyRightYears = computed(() => {
  const publicDate = 2024;
  const currentDate = new Date().getFullYear();
  if (publicDate === currentDate) {
    return publicDate;
  }

  return `${publicDate} - ${currentDate}`;
});

</script>

<style scoped src="./LayoutFooter.scss" />
