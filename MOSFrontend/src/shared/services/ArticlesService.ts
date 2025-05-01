import {
  IArticlesService
} from "@/shared/services/base";
import { IArticle } from "@/shared/types";
import ServiceBase from "@/shared/services/ServiceBase.ts";
import { TCreateArticleRequest } from "@/shared/services/base/IArticlesService.ts";

class ArticlesService extends ServiceBase implements IArticlesService {
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
