<template>
  <section class="flat-layout">
    <header>
      <LayoutHeader />
    </header>

    <article>
      <slot />
    </article>
  </section>
</template>

<script setup lang="ts">
import LayoutHeader from "@/layouts/components/LayoutHeader/LayoutHeader.vue";
</script>

<style scoped src="./FlatLayout.scss" />
