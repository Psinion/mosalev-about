export interface PsiDictionary<T> {
  [key: string]: T;
}

export function getKeyByValue<T>(dictionary: PsiDictionary<T>, value: T) {
  return Object.keys(dictionary).find(key => dictionary[key] === value);
}
