import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IArticle } from "@/shared/types";

export interface IArticlesService extends IServiceBase {
  createArticle(payload: TCreateArticleRequest): Promise<IArticle>;
}

export type TCreateArticleRequest = {
  projectId?: number;
  title: string;
  description: string;
};
