import { RouteParamsRaw } from "vue-router";

export interface IDictionary<T> {
  [key: string]: T;
}

export type TRoute = {
  name?: string;
  params?: RouteParamsRaw;
};
