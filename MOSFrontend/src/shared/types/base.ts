import { RouteParamsRaw } from "vue-router";
import { IArticle } from "@/shared/types/project.ts";

export type TRoute = {
  name?: string;
  params?: RouteParamsRaw;
  query?: RouteParamsRaw;
};

export interface IPagination<T> {
  items: T[];
  totalCount: number;
}
