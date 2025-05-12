import { config } from "@vue/test-utils";
import PsiButton from "@/shared/PsiUI/components/PsiButton/PsiButton.vue";
import i18nInstance from "@/setup/i18n.ts";

config.global.plugins = [
  i18nInstance
];

// Мокируем глобальные компоненты
config.global.components = {
  PsiButton: { template: PsiButton }
};

// Мокируем директивы
config.global.directives = {
  tooltip: {
    mounted: vi.fn(),
    updated: vi.fn()
  }
};
