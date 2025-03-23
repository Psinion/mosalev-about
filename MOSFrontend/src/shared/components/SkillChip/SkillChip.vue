<template>
  <div class="skill-chip">
    {{ name }}
  </div>
</template>

<script setup lang="ts">

const props = defineProps({
  name: {
    type: String,
    required: true
  }
});
</script>

<style scoped src="./SkillChip.scss" />
