export function setPageTitle(title?: string) {
  const appName = "Mosalev";
  const titleTotal = title ? `${title} | ${appName}` : appName;
  document.title = titleTotal as string;
}
