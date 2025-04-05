import js from "@eslint/js";
import ts from "typescript-eslint";
import pluginVue from "eslint-plugin-vue";
import stylistic from "@stylistic/eslint-plugin";

export default [
  js.configs.recommended,
  ...ts.configs.recommended,
  ...pluginVue.configs["flat/recommended"],
  {
    files: ["src/**/*.{js,ts}", "*.vue", "**/*.vue"],
    languageOptions: {
      parserOptions: {
        parser: "@typescript-eslint/parser"
      }
    },
    rules: {
      // "no-unused-vars": "off"
      "vue/html-self-closing": ["error", {
        html: {
          void: "always",
          normal: "any",
          component: "any"
        },
        svg: "always",
        math: "always"
      }]
    }
  },
  stylistic.configs["disable-legacy"],
  stylistic.configs.customize({
    indent: 2,
    quotes: "double",
    semi: true,
    commaDangle: "never"
  })
];
