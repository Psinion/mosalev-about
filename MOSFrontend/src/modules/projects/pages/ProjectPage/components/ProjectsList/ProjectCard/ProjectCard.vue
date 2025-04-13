<template>
  <section class="project-card">
    <h3>{{ title }}</h3>
    <div>
      {{ description }}
    </div>
  </section>
</template>

<script setup lang="ts">

const props = defineProps({
  title: {
    type: String,
    required: true
  },
  description: {
    type: String,
    required: true
  }
});

</script>

<style scoped src="./ProjectCard.scss" />
