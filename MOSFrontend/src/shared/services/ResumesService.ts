import { type IResumesService, TPinResumeRequest, TUpdateResumeRequest } from "@/shared/services/base";
import { useRequestor } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { TResume, TResumeCompact } from "@/shared/types/resume.ts";
import { TCreateResumeRequest } from "@/shared/services/base/IResumesService.ts";

class ResumesService implements IResumesService {
  private requestor = useRequestor();

  async getResumesList(): Promise<TResumeCompact[]> {
    try {
      return await this.requestor.get("api/v1/resumes/list") as TResumeCompact[];
    }
    catch (error) {
      throw error;
    }
  }

  async getResume(resumeId: number): Promise<TResume> {
    try {
      return await this.requestor.get(`api/v1/resumes/${resumeId}`) as TResume;
    }
    catch (error) {
      throw error;
    }
  }

  async createResume(params: TCreateResumeRequest): Promise<TResume> {
    try {
      return await this.requestor.post("api/v1/resumes", params) as TResume;
    }
    catch (error) {
      throw error;
    }
  }

  async updateResume(params: TUpdateResumeRequest): Promise<TResume> {
    try {
      return await this.requestor.put("api/v1/resumes", params) as TResume;
    }
    catch (error) {
      throw error;
    }
  }

  async hasPinnedResume(): Promise<boolean> {
    try {
      await this.requestor.head(`api/v1/resumes/pin`);
      return true;
    }
    catch {
      return false;
    }
  }

  async getPinnedResume(): Promise<TResume> {
    try {
      return await this.requestor.get(`api/v1/resumes/pin`) as TResume;
    }
    catch (error) {
      throw error;
    }
  }

  async pinResume(resumeId: number): Promise<void> {
    try {
      await this.requestor.put(`api/v1/resumes/${resumeId}/pin`);
    }
    catch (error) {
      throw error;
    }
  }

  async unpinResume(resumeId: number): Promise<void> {
    try {
      await this.requestor.put(`api/v1/resumes/${resumeId}/unpin`);
    }
    catch (error) {
      throw error;
    }
  }
}

const ResumesServiceInstance = new ResumesService();

export default ResumesServiceInstance;
