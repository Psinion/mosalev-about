<template>
  <Transition name="psi-transition-fade">
    <div v-show="visible">
      <slot></slot>
    </div>
  </Transition>
</template>

<script setup lang="ts">
import { computed, PropType } from "vue";
import { TPsiTransitionFadeLeaveTransition } from "@/shared/PsiUI/components/PsiTransitionFade/types.ts";

const props = defineProps({
  visible: {
    type: Boolean,
    default: false
  },
  leaveSpeed: {
    type: String as PropType<TPsiTransitionFadeLeaveTransition>,
    default: "default"
  }
});

const leaveTransition = computed(() => {
  switch (props.leaveSpeed) {
    case "instant": return "none";
    case "fast": return "var(--fast-transition)";
    case "default": return "var(--default-transition)";
    default: return "none";
  }
});
</script>

<style lang="scss" scoped>
.psi-transition-fade-enter-active {
  transition: var(--default-transition);
}

.psi-transition-fade-leave-active {
  transition: v-bind(leaveTransition);
}

.psi-transition-fade-enter-from,
.psi-transition-fade-leave-to {
  opacity: 0;
}
</style>
