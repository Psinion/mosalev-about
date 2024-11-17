let lastComponentId = 0;

export function useComponentId(prefix: string = "PsiComponent") {
  return `${prefix}${lastComponentId++}`;
}
