import { createHighlighterCore } from "shiki/core";
import { createOnigurumaEngine } from "shiki/engine-oniguruma.mjs";

export const highlighterPromise = createHighlighterCore({
  themes: [
    () => import("@shikijs/themes/min-dark")
  ],
  langs: [
    () => import("@shikijs/langs/csharp")
  ],
  engine: createOnigurumaEngine(import("shiki/wasm"))
});
