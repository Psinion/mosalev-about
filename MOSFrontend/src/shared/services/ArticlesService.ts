import {
  IArticlesService, TChangeArticleProjectRequest, TGetCompactArticlesByProjectRequest
} from "@/shared/services/base";
import { IArticle, IArticlesPagination, IProjectCompact } from "@/shared/types";
import ServiceBase from "@/shared/services/ServiceBase.ts";
import {
  TChangeArticleVisibilityRequest,
  TCreateArticleRequest,
  TGetCompactArticlesRequest,
  TUpdateArticleRequest
} from "@/shared/services/base/IArticlesService.ts";
import { OperationResult, Result } from "@/shared/PsiUI/utils/operationResults.ts";

class ArticlesService extends ServiceBase implements IArticlesService {
  async getCompactArticles(payload: TGetCompactArticlesRequest): Promise<IArticlesPagination> {
    try {
      return await this.requestor.get("api/v1/articles", payload) as IArticlesPagination;
    }
    catch (error) {
      throw error;
    }
  }

  async getCompactArticlesByProject(payload: TGetCompactArticlesByProjectRequest): Promise<IArticlesPagination> {
    try {
      return await this.requestor.get("api/v1/articles/by-project", payload) as IArticlesPagination;
    }
    catch (error) {
      throw error;
    }
  }

  async getArticle(articleId: number): Promise<IArticle> {
    try {
      return await this.requestor.get(`api/v1/articles/${articleId}`) as IArticle;
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

  async updateArticle(articleId: number, payload: TUpdateArticleRequest): Promise<IArticle> {
    try {
      return await this.requestor.put(`api/v1/articles/${articleId}`, payload) as IArticle;
    }
    catch (error) {
      throw error;
    }
  }

  async deleteArticle(articleId: number): Promise<void> {
    try {
      await this.requestor.delete(`api/v1/articles/${articleId}`);
    }
    catch (error) {
      throw error;
    }
  }

  async changeArticleVisibility(articleId: number, payload: TChangeArticleVisibilityRequest): Promise<void> {
    try {
      await this.requestor.patch(`api/v1/articles/${articleId}/visibility`, payload);
    }
    catch (error) {
      throw error;
    }
  }

  async changeArticleProject(articleId: number, payload: TChangeArticleProjectRequest): Promise<OperationResult<void>> {
    try {
      await this.requestor.patch(`api/v1/articles/${articleId}/project`, payload);
      return Result.success();
    }
    catch (error) {
      return Result.failure(error.message);
    }
  }
}

const ArticlesServiceInstance = new ArticlesService();

export default ArticlesServiceInstance;
