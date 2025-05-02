<template>
  <oh-vue-icon
    class="psi-icon"
    :name="iconCode"
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
import { computed, PropType } from "vue";
import { TPsiIcon, TPsiIconFlip } from "@/shared/PsiUI/components/PsiIcon/types.ts";

const props = defineProps({
  icon: {
    type: String as PropType<TPsiIcon>,
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
    type: String as PropType<TPsiIconFlip>,
    default: null
  },
  speed: {
    type: String,
    default: null
  }
});

const ICON_CODES = {
  "pin": "bi-pin-angle",
  "pin-filled": "bi-pin-angle-fill",
  "trash-box": "md-deleteforever",
  "close": "md-close",
  "telegram": "fa-telegram",
  "email": "md-email-round",
  "steam": "fa-steam",
  "github": "fa-github",
  "error": "md-reportgmailerrorred",
  "success": "oi-check-circle",
  "edit": "md-edit",
  "eye": "hi-solid-eye",
  "eye-crossed": "hi-solid-eye-off",
  "left": "md-chevronleft",
  "right": "md-chevronright"
};

const iconCode = computed(() => ICON_CODES[props.icon]);

</script>

<style scoped src="./PsiIcon.scss" />
