import { useI18n } from "vue-i18n";

const SIZES_TABLE = [
  { name: "kb", size: 1024 },
  { name: "mb", size: 1024 * 1024 },
  { name: "gb", size: 1024 * 1024 * 1024 },
  { name: "tb", size: 1024 * 1024 * 1024 * 1024 }
];

export function useFileFormatter() {
  const { t } = useI18n();

  const fileSize2Text = (fileSize: number) => {
    let i = 0;
    for (; i < SIZES_TABLE.length - 1; i++) {
      const element = SIZES_TABLE[i];
      const size = fileSize / element.size;
      if (size < 100) {
        return `${size.toFixed(2)} ${t(`files.fileSize.${element.name}`)}`;
      }
    }

    const element = SIZES_TABLE[i];
    const size = fileSize / element.size;
    return `${size.toFixed(2)} ${t(`files.fileSize.${element.name}`)}`;
  };

  return { fileSize2Text };
}
