/// <reference types="vitest/config" />
import { fileURLToPath, URL } from "node:url";
import { defineConfig } from "vite";
import vue from "@vitejs/plugin-vue";

export default defineConfig({
  plugins: [
    vue()
  ],
  resolve: {
    alias: {
      "@": fileURLToPath(new URL("./src", import.meta.url)),
      "@scss-mixins": fileURLToPath(new URL("./src/assets/styles/mixins/", import.meta.url))
    }
  },
  css: {
    preprocessorOptions: {
      scss: { api: "modern-compiler" }
    }
  },
  test: {
    globals: true,
    environment: "jsdom",
    setupFiles: "src/tests/vitest.setup.ts"
  }
});
