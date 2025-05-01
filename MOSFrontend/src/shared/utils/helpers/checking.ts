export function checkNumber(value: string | undefined | null): boolean {
  if (!value) {
    return false;
  }

  const num = +value;
  return !Number.isNaN(num) && value.trim() !== "";
}

export function checkPositiveNumber(value: string | undefined | null): boolean {
  if (!value) {
    return false;
  }

  const num = +value;
  return !Number.isNaN(num) && value.trim() !== "" && num >= 0;
}
