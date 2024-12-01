import { useI18n } from "vue-i18n";

export default function useValidationRules() {
  const { t } = useI18n();

  function required(value?: string | number | Date | null) {
    if (value === undefined || value === null) {
      return t("psiUi.requiredErrorMessage");
    }

    return true;
  }

  return {
    required
  };
}
