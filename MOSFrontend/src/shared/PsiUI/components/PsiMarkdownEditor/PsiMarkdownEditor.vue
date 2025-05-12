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
import { PropType, ref, watch } from "vue";
import { markedInstance } from "@/setup/marked.ts";

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

const compiledMarkdown = ref("");
watch(() => props.modelValue, async (value) => {
  compiledMarkdown.value = await markedInstance.parse(value ?? "");
}, {
  immediate: true
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
