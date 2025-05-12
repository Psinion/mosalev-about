import { Directive } from "vue";
import { markedInstance } from "@/setup/marked.ts";

export const markdownDirective: Directive = {
  async mounted(el: HTMLElement, binding) {
    await updateContent(el, binding.value ?? el.textContent);
  },

  async updated(el: HTMLElement, binding) {
    await updateContent(el, binding.value ?? el.textContent);
  }
};

async function updateContent(el: HTMLElement, content: string) {
  if (!content) {
    return;
  }

  try {
    el.innerHTML = await markedInstance.parse(content);
  }
  catch (error) {
    console.error("Markdown directive error:", error);
  }
}
