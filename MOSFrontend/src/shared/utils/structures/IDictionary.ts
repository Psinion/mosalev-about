export interface IDictionary<T> {
  [key: string]: T;
}

export function getKeyByValue<T>(dictionary: IDictionary<T>, value: T) {
  return Object.keys(dictionary).find(key => dictionary[key] === value);
}
