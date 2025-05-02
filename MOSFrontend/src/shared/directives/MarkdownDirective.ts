import { Directive } from "vue";
import { marked } from "marked";

export const markdownDirective: Directive = {
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
    el.innerHTML = marked(content) as string;
  }
  catch (error) {
    console.error("Markdown directive error:", error);
  }
}
