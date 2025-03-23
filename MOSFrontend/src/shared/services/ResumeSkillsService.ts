import { useRequestor } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { ResumeSkill } from "@/shared/types/resume.ts";
import { IResumeSkillsService, TCreateResumeSkillRequest, TUpdateResumeSkillRequest } from "@/shared/services/base";

class ResumeSkillsService implements IResumeSkillsService {
  private requestor = useRequestor();

  async createResumeSkill(params: TCreateResumeSkillRequest): Promise<ResumeSkill> {
    try {
      return await this.requestor.post("api/v1/ResumeSkills", params) as ResumeSkill;
    }
    catch (error) {
      throw error;
    }
  }

  async updateResumeSkill(params: TUpdateResumeSkillRequest): Promise<ResumeSkill> {
    try {
      return await this.requestor.put("api/v1/ResumeSkills", params) as ResumeSkill;
    }
    catch (error) {
      throw error;
    }
  }

  async deleteResumeSkill(skillId: number): Promise<boolean> {
    try {
      await this.requestor.delete(`api/v1/ResumeSkills/${skillId}`);
      return true;
    }
    catch {
      return false;
    }
  }
}

const ResumesSkillsServiceInstance = new ResumeSkillsService();

export default ResumesSkillsServiceInstance;
