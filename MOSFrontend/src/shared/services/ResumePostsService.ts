import { useRequestor } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { IResumeCompanyEntryPost } from "@/shared/types/resume.ts";
import { IResumePostsService, TCreateResumePostRequest, TUpdateResumePostRequest } from "@/shared/services/base";

class ResumePostsService implements IResumePostsService {
  private requestor = useRequestor();

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
