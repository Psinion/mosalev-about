import dayjs from "dayjs";
import "dayjs/locale/ru";
import "dayjs/locale/en";

dayjs.locale("ru");

export function formatDate(date: string | Date, format: string = "DD.MM.YYYY") {
  return dayjs(date).format(format);
}
