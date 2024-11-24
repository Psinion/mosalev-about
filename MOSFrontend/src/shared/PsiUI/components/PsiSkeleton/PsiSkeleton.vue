<template>
  <section
    class="psi-skeleton"
    :style="styles"
  />
</template>

<script setup lang="ts">

import { computed } from "vue";

const props = defineProps({
  width: {
    type: String,
    required: true
  },
  height: {
    type: String,
    required: true
  },
  marginTop: {
    type: String,
    default: "0px"
  }
});

const styles = computed(() => {
  return [{
    "width": props.width,
    "height": props.height,
    "margin-top": props.marginTop
  }];
});

</script>

<style scoped src="./PsiSkeleton.scss" />
