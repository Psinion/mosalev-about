import { useI18n } from "vue-i18n";

export default function useValidationRules(settings?: ValidationSettings) {
  const { t } = useI18n();

  function required(value?: string | number | Date | null) {
    if (value === undefined || value === null || (typeof value == "string" && value === "")) {
      return t("psiUi.requiredErrorMessage");
    }

    return true;
  }

  function minLength(value?: string | Array<unknown> | null) {
    const minLengthValue = settings?.minLength ?? 0;

    if (value === null || value === undefined) {
      return true;
    }

    if (typeof value !== "string" && !Array.isArray(value)) {
      return t("psiUi.incorrectDataErrorMessage");
    }

    if (value.length < minLengthValue) {
      return t("psiUi.minLengthErrorMessage");
    }

    return true;
  }

  function maxLength(value?: string | Array<unknown> | null) {
    const maxLengthValue = settings?.maxLength ?? Number.MAX_VALUE;

    if (value === null || value === undefined) {
      return true;
    }

    if (typeof value !== "string" && !Array.isArray(value)) {
      return t("psiUi.incorrectDataErrorMessage");
    }

    if (value.length > maxLengthValue) {
      return t("psiUi.maxLengthErrorMessage");
    }

    return true;
  }

  return {
    required,
    minLength,
    maxLength
  };
}

export interface ValidationSettings {
  minLength?: number;
  maxLength?: number;
}
