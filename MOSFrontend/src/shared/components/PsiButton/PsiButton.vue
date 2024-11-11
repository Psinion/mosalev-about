<template>
  <button class="psi-button">
    <template v-if="!route">
      <slot />
    </template>
    <RouterLink
      v-else
      class="link"
      :to="route"
    >
      <slot />
    </RouterLink>
  </button>
</template>

<script setup lang="ts">

import { PropType } from "vue";
import { TRoute } from "@/shared/types";

const props = defineProps({
  route: {
    type: Object as PropType<TRoute>,
    default: null
  }
});

</script>

<style scoped src="./PsiButton.scss" />
