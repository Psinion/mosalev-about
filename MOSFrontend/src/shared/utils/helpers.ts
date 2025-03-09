export function setPageTitle(title?: string) {
  const appName = "Mosalev";
  const titleTotal = title ? `${title} | ${appName}` : appName;
  document.title = titleTotal as string;
}

export function date2DateOnly(date: Date) {
  return date.toISOString().split("T")[0];
}
