import { createI18n } from "vue-i18n";
import { en, ru } from "@/locales/locales.ts";

const instance = createI18n({
  legacy: false,
  locale: "ru",
  fallbackLocale: "ru",
  messages: {
    en: en,
    ru: ru
  }
});

export default instance;

export const i18n = instance.global;
