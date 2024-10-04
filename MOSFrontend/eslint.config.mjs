import ESLint from "@eslint/js";
import TSEslint from "typescript-eslint";
import pluginVue from "eslint-plugin-vue";
import stylistic from "@stylistic/eslint-plugin";

export default [
    ESLint.configs.recommended,
    ...TSEslint.configs.recommended,
    ...pluginVue.configs["flat/recommended"],
    {
        files: ["src/**/*.{js,ts}"]
        /* rules: {
            indent: ["error", 2]
        } */
    },
    stylistic.configs["disable-legacy"],
    stylistic.configs.customize({
        indent: 4,
        quotes: "double",
        semi: true,
        commaDangle: "never"
    })
];
