const SIZES_TABLE = [
  { name: "Кб", size: 1024 },
  { name: "Мб", size: 1024 * 1024 },
  { name: "Гб", size: 1024 * 1024 * 1024 },
  { name: "Тб", size: 1024 * 1024 * 1024 * 1024 }
];

export function fileSize2Text(fileSize: number) {
  let i = 0;
  for (; i < SIZES_TABLE.length - 1; i++) {
    const element = SIZES_TABLE[i];
    const size = fileSize / element.size;
    if (size < 100) {
      return `${size.toFixed(2)} ${element.name}`;
    }
  }

  const element = SIZES_TABLE[i];
  const size = fileSize / element.size;
  return `${size.toFixed(2)} ${element.name}`;
}
