import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IArticle, IArticlesPagination } from "@/shared/types";

export interface IArticlesService extends IServiceBase {
  getCompactArticles(payload: TGetCompactArticlesRequest): Promise<IArticlesPagination>;
  getCompactArticlesByProject(payload: TGetCompactArticlesByProjectRequest): Promise<IArticlesPagination>;
  getArticle(articleId: number): Promise<IArticle>;
  createArticle(payload: TCreateArticleRequest): Promise<IArticle>;
  updateArticle(articleId: number, payload: TUpdateArticleRequest): Promise<IArticle>;
  deleteArticle(articleId: number): Promise<void>;
  changeArticleVisibility(articleId: number, payload: TChangeArticleVisibilityRequest): Promise<void>;
}

export type TGetCompactArticlesRequest = {
  onlyFree?: boolean;
  limit?: number;
  offset?: number;
};

export type TGetCompactArticlesByProjectRequest = {
  projectId: number;
  limit?: number;
  offset?: number;
};

export type TCreateArticleRequest = {
  projectId?: number;
  title: string;
  description: string;
};

export type TUpdateArticleRequest = {
  projectId?: number;
  title: string;
  description: string;
};

export type TChangeArticleVisibilityRequest = {
  visible: boolean;
};
