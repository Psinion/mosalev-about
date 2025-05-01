import { RouteParamsRaw } from "vue-router";

export type TRoute = {
  name?: string;
  params?: RouteParamsRaw;
  query?: RouteParamsRaw;
};
