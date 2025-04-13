import { IResumeCompanyEntryPost } from "@/shared/types/resume.ts";
import { IResumePostsService, TCreateResumePostRequest, TUpdateResumePostRequest } from "@/shared/services/base";
import ServiceBase from "@/shared/services/ServiceBase.ts";

class ResumePostsService extends ServiceBase implements IResumePostsService {
  async createResumePost(params: TCreateResumePostRequest): Promise<IResumeCompanyEntryPost> {
    try {
      return await this.requestor.post("api/v1/ResumePosts", params) as IResumeCompanyEntryPost;
    }
    catch (error) {
      throw error;
    }
  }

  async updateResumePost(params: TUpdateResumePostRequest): Promise<IResumeCompanyEntryPost> {
    try {
      return await this.requestor.put("api/v1/ResumePosts", params) as IResumeCompanyEntryPost;
    }
    catch (error) {
      throw error;
    }
  }

  async deleteResumePost(postId: number): Promise<boolean> {
    try {
      await this.requestor.delete(`api/v1/ResumePosts/${postId}`);
      return true;
    }
    catch {
      return false;
    }
  }
}

const ResumePostsServiceInstance = new ResumePostsService();

export default ResumePostsServiceInstance;
