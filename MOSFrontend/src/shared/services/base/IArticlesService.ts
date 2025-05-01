import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IArticle, IArticlesPagination } from "@/shared/types";

export interface IArticlesService extends IServiceBase {
  getCompactArticles(payload: TGetCompactArticlesRequest): Promise<IArticlesPagination>;
  createArticle(payload: TCreateArticleRequest): Promise<IArticle>;
}

export type TGetCompactArticlesRequest = {
  projectId?: number;
};

export type TCreateArticleRequest = {
  projectId?: number;
  title: string;
  description: string;
};
