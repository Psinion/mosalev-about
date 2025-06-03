import { Marked } from "marked";
import markedShiki from "marked-shiki";
import { highlighterPromise } from "@/setup/shiki.ts";
import {
  transformerNotationDiff,
  transformerNotationFocus,
  transformerNotationHighlight
} from "@shikijs/transformers";

export const markedInstance = new Marked()
  .use(
    markedShiki({
      async highlight(code, lang, props) {
        const highlighter = await highlighterPromise;
        return highlighter.codeToHtml(code, {
          colorReplacements: undefined,
          grammarContextCode: undefined,
          grammarState: undefined,
          tokenizeMaxLineLength: undefined,
          tokenizeTimeLimit: undefined,
          lang,
          theme: "min-dark",
          meta: { __raw: props.join(" ") }, // required by `transformerMeta*`
          transformers: [
            transformerNotationDiff({
              matchAlgorithm: "v3"
            }),
            transformerNotationHighlight({
              matchAlgorithm: "v3"
            }),
            transformerNotationFocus({
              matchAlgorithm: "v3"
            })
          ]
        });
      }
    })
  );
