import { createI18n } from "vue-i18n";
import { en, ru } from "@/locales/locales.ts";

const i18nInstance = createI18n({
  legacy: false,
  locale: "ru",
  fallbackLocale: "ru",
  messages: {
    en: en,
    ru: ru
  }
});

export default i18nInstance;

export const i18n = i18nInstance.global;
