import {
  IArticlesService
} from "@/shared/services/base";
import { IArticle, IArticlesPagination } from "@/shared/types";
import ServiceBase from "@/shared/services/ServiceBase.ts";
import { TCreateArticleRequest, TGetCompactArticlesRequest } from "@/shared/services/base/IArticlesService.ts";

class ArticlesService extends ServiceBase implements IArticlesService {
  async getCompactArticles(payload: TGetCompactArticlesRequest): Promise<IArticlesPagination> {
    try {
      return await this.requestor.get("api/v1/articles", payload) as IArticlesPagination;
    }
    catch (error) {
      throw error;
    }
  }

  async createArticle(payload: TCreateArticleRequest): Promise<IArticle> {
    try {
      return await this.requestor.post("api/v1/articles", payload) as IArticle;
    }
    catch (error) {
      throw error;
    }
  }
}

const ArticlesServiceInstance = new ArticlesService();

export default ArticlesServiceInstance;
