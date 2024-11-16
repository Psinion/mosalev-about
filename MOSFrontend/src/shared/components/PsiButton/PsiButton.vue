<template>
  <button
    class="psi-button"
    :class="{'disabled': disabled}"
    :type="type"
  >
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
import { TPsiButtonType } from "@/shared/components/PsiButton/types.ts";

const props = defineProps({
  type: {
    type: String as PropType<TPsiButtonType>,
    default: "button"
  },
  route: {
    type: Object as PropType<TRoute>,
    default: null
  },
  disabled: {
    type: Boolean,
    default: false
  }
});

</script>

<style scoped src="./PsiButton.scss" />
