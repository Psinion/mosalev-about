import { computed, Ref } from "vue";
import { useI18n } from "vue-i18n";
import { formatDate } from "@/shared/utils/dateHelpers.ts";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { AppLocale } from "@/shared/enums/common.ts";

export const useShortDateCreateUpdate2String = (
  createdAt: Ref<Date | string | null | undefined>,
  updateAt: Ref<Date | string | null | undefined>
) => {
  const userStore = useUserStore();
  const { t } = useI18n();

  const dateFormat = computed(() => userStore.locale === AppLocale.ru ? "D MMMM, YYYY" : "MMMM D, YYYY");

  const dateUpdateString = computed(() =>
    updateAt.value ? formatDate(updateAt.value, dateFormat.value) : null
  );

  const dateCreateString = computed(() => {
    const formattedDate = formatDate(createdAt.value, dateFormat.value);
    return dateUpdateString.value ? `${formattedDate} (${t("forms.editMark")})` : formattedDate;
  });

  return {
    dateUpdateString,
    dateCreateString
  };
};

export const useDetailDateCreateUpdate2String = (
  createdAt: Ref<Date | string | null | undefined>,
  updateAt: Ref<Date | string | null | undefined>
) => {
  const { t } = useI18n();

  const dateUpdateString = computed(() =>
    updateAt.value ? formatDate(updateAt.value, "YYYY.MM.DD HH:mm") : null
  );

  const dateCreateString = computed(() => {
    const formattedDate = formatDate(createdAt.value, "YYYY.MM.DD HH:mm");
    return dateUpdateString.value ? `${formattedDate} (${t("forms.editMark")} ${dateUpdateString.value})` : formattedDate;
  });

  return {
    dateString: dateCreateString
  };
};
