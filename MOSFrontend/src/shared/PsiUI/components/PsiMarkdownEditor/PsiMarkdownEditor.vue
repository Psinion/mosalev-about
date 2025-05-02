<template>
  <section
    class="psi-markdown-editor"
    :class="{'disabled': disabled}"
  >
    <div
      class="editor"
      :style="{height: height}"
    >
      <textarea
        :value="modelValue"
        class="caption-regular"
        autocomplete="off"
        @focus="onFocus"
        @blur="onBlur"
        @input="onInput($event.target as HTMLInputElement)"
      />

      <div
        class="preview typography-block"
        v-html="compiledMarkdown"
      />
    </div>
  </section>
</template>

<script setup lang="ts">
import { computed, PropType } from "vue";
import { marked } from "marked";

const props = defineProps({
  modelValue: {
    type: String as PropType<string | undefined>,
    default: undefined
  },
  height: {
    type: String,
    default: "400px"
  },
  disabled: {
    type: Boolean,
    default: false
  }
});

const emit = defineEmits({
  "update:modelValue": (value: string | undefined) => true,
  "focus": () => true,
  "blur": () => true
});

const compiledMarkdown = computed(() => {
  return marked(props.modelValue ?? "");
});

function onFocus() {
  emit("focus");
}

function onBlur() {
  emit("blur");
}

function onInput(target: HTMLInputElement) {
  emit("update:modelValue", target?.value ? target.value : undefined);
}

</script>

<style scoped src="./PsiMarkdownEditor.scss" />
