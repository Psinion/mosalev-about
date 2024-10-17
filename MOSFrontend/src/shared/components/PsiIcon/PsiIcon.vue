<template>
  <oh-vue-icon
    :name="icon"
    :scale="scale"
    :fill="fill"
    :flip="flip"
    :animation="animation"
  >
    <slot />
  </oh-vue-icon>
</template>

<script setup lang="ts">
import { OhVueIcon } from "oh-vue-icons";
import { PropType } from "vue";
import { TIcon, TIconFlip } from "@/shared/components/PsiIcon/types.ts";

defineProps({
  icon: {
    type: String as PropType<TIcon>,
    required: true
  },
  fill: {
    type: String,
    default: null
  },
  scale: {
    type: Number,
    default: 1
  },
  animation: {
    type: String,
    default: null
  },
  hover: {
    type: Boolean,
    default: false
  },
  flip: {
    type: String as PropType<TIconFlip>,
    default: null
  },
  speed: {
    type: String,
    default: null
  }
});

</script>

<style scoped src="./PsiIcon.scss" />
