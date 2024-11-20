import { type IResumesService } from "@/shared/services/base";
import { useRequestor } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { TResume, TResumeCompact } from "@/shared/types/resume.ts";
import { TCreateResumeRequest } from "@/shared/services/base/IResumesService.ts";

class ResumesService implements IResumesService {
  private requestor = useRequestor();

  async createResume(params: TCreateResumeRequest): Promise<TResume> {
    try {
      return await this.requestor.post("api/v1/resumes/resume", params) as TResume;
    }
    catch (error) {
      throw error;
    }
  }

  async getResumes(): Promise<TResumeCompact[]> {
    try {
      return await this.requestor.get("api/v1/resumes/resume/list") as TResumeCompact[];
    }
    catch (error) {
      throw error;
    }
  }
}

const ResumesServiceInstance = new ResumesService();

export default ResumesServiceInstance;
