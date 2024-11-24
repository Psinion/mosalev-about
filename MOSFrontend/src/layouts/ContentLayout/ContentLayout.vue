<template>
  <section class="content-layout">
    <header>
      <LayoutHeader />
    </header>

    <article>
      <slot />
    </article>

    <footer>
      <LayoutFooter />
    </footer>
  </section>
</template>

<script setup lang="ts">
import LayoutHeader from "@/layouts/components/LayoutHeader/LayoutHeader.vue";
import LayoutFooter from "@/layouts/components/LayoutFooter/LayoutFooter.vue";
</script>

<style scoped src="./ContentLayout.scss" />
