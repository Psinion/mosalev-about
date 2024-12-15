import { useRequestor } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { TResumeCompanyEntry } from "@/shared/types/resume.ts";
import {
  IResumeCompanyEntriesService,
  TCreateResumeCompanyEntryRequest, TUpdateResumeCompanyEntryRequest
} from "@/shared/services/base/IResumeCompanyEntriesService.ts";

class ResumeCompanyEntriesService implements IResumeCompanyEntriesService {
  private requestor = useRequestor();

  async createResumeCompanyEntry(params: TCreateResumeCompanyEntryRequest): Promise<TResumeCompanyEntry> {
    try {
      return await this.requestor.post("api/v1/ResumeCompanyEntries", params) as TResumeCompanyEntry;
    }
    catch (error) {
      throw error;
    }
  }

  async updateResumeCompanyEntry(params: TUpdateResumeCompanyEntryRequest): Promise<TResumeCompanyEntry> {
    try {
      return await this.requestor.put("api/v1/ResumeCompanyEntries", params) as TResumeCompanyEntry;
    }
    catch (error) {
      throw error;
    }
  }
}

const ResumesCompanyEntriesServiceInstance = new ResumeCompanyEntriesService();

export default ResumesCompanyEntriesServiceInstance;
