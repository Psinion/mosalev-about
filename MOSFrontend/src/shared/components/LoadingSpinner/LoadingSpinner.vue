<template>
  <Transition name="loading-spinner">
    <section
      v-show="visible"
      class="loading-spinner"
    >
      <div class="central-box">
        <div class="star celestial-object" />
        <div class="orbit-1 celestial-object" />
        <div class="orbit-2 celestial-object" />
        <div class="orbit-3 celestial-object" />
      </div>
    </section>
  </Transition>
</template>

<script setup lang="ts">

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  }
});

</script>

<style scoped src="./LoadingSpinner.scss" />
