<template>
  <section
    class="layout-header-button"
    :class="{disabled: disabled}"
  >
    <RouterLink
      class="title "
      :to="route"
    >
      <h3>{{ title }}</h3>
    </RouterLink>
  </section>
</template>

<script setup lang="ts">
import { TRoute } from "@/shared/types";
import { PropType } from "vue";

const props = defineProps({
  title: {
    type: String,
    required: true
  },
  route: {
    type: Object as PropType<TRoute>,
    default() {
      return {};
    }
  },
  disabled: {
    type: Boolean,
    default: false
  }
});
</script>

<style scoped src="./LayoutHeaderButton.scss" />
