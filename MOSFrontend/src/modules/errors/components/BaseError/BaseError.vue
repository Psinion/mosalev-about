<template>
  <article class="base-error">
    <h1 v-if="statusCode">
      {{ statusCode }}
    </h1>
    <PsiIcon
      v-else
      :icon="icon"
    />
    <div class="error-placeholder">
      <h2>{{ header }}</h2>
      <div
        v-if="description"
        class="caption-regular error-description"
      >
        {{ description }}
      </div>
    </div>
  </article>
</template>

<script setup lang="ts">
import { type PropType } from "vue";
import { TPsiIcon } from "@/shared/PsiUI/components/PsiIcon/types.ts";
import PsiIcon from "@/shared/PsiUI/components/PsiIcon/PsiIcon.vue";

const props = defineProps({
  statusCode: {
    type: Number,
    default: null
  },
  icon: {
    type: String as PropType<TPsiIcon>,
    default: null
  },
  header: {
    type: String,
    required: true
  },
  description: {
    type: String,
    default: null
  }
});
</script>

<style lang="scss" scoped src="./BaseError.scss" />
