import { computed, Ref } from "vue";
import { useI18n } from "vue-i18n";
import { formatDate } from "@/shared/utils/dateHelpers.ts";

export const useDateCreateEdit2String = (createdAt: Ref<Date | string | null | undefined>, updateAt: Ref<Date | string | null | undefined>) => {
  const { t } = useI18n();

  const dateUpdateString = computed(() =>
    updateAt.value ? formatDate(updateAt.value, "YYYY.MM.DD HH:mm") : ""
  );

  const dateCreateString = computed(() => {
    const formattedDate = formatDate(createdAt.value, "YYYY.MM.DD HH:mm");
    return dateUpdateString.value ? `${formattedDate} (${t("forms.editMark")})` : formattedDate;
  });

  return {
    dateUpdateString,
    dateCreateString
  };
};
