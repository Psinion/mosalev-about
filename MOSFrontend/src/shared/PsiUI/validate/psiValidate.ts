import { inject, ref, watch } from "vue";

export type MaybeArray<T> = T | T[];
export type MaybePromise<T> = T | Promise<T>;
export type PsiValidateFunction<TValue> = (value: TValue) => MaybePromise<boolean | MaybeArray<string>>;
export type PsiValidator = () => boolean;
export type PsiResetter = () => void;
export type RegisterValidatorFunction = (validator: PsiValidator, resetter: PsiResetter) => void;
export type NotifyValidityFunction = (validator: PsiValidator, isValid: boolean) => void;
type TRuleExpression<TValue> = PsiValidateFunction<TValue> | PsiValidateFunction<TValue>[];

type PsiValidationSettings<TValue> = {
  initialValue?: TValue;
};

export function usePsiValidation<TValue>(rules: TRuleExpression<TValue>, settings: PsiValidationSettings<TValue>) {
  const value = ref(settings.initialValue);
  const touched = ref(false);
  const errorMessage = ref("");

  watch(() => value.value, () => {
    if (touched.value) {
      validate();
    }
  });

  const notifyValidity = inject<NotifyValidityFunction>("notifyValidity");

  function validate() {
    errorMessage.value = "";
    let isValid = true;

    if (Array.isArray(rules)) {
      for (const rule of rules) {
        const result = rule(value.value);

        if (typeof result === "string") {
          errorMessage.value = result;
          isValid = false;
          break;
        }
      }
    }
    else {
      const result = rules(value.value);

      if (typeof result === "string") {
        errorMessage.value = result;
        isValid = false;
      }
    }

    if (notifyValidity) {
      notifyValidity(validate, isValid);
    }
    return isValid;
  }

  function handleBlur() {
    if (!touched.value) {
      touched.value = true;
    }
    validate();
  }

  function reset() {
    touched.value = false;
    errorMessage.value = "";
    value.value = settings.initialValue;
  }

  return { value, errorMessage, validate, handleBlur, reset };
}
