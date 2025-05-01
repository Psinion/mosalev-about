import { Directive } from "vue";
import { marked } from "marked";

export const MarkdownDirective: Directive = {
  mounted(el: HTMLElement, binding) {
    updateContent(el, binding.value ?? el.textContent);
  },

  updated(el: HTMLElement, binding) {
    updateContent(el, binding.value ?? el.textContent);
  }
};

function updateContent(el: HTMLElement, content: string) {
  if (!content) {
    return;
  }

  try {
    el.innerHTML = marked(content);
  }
  catch (error) {
    console.error("Markdown directive error:", error);
  }
}
