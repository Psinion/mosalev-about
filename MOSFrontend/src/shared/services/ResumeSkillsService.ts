import { IResumeSkill } from "@/shared/types/resume.ts";
import { IResumeSkillsService, TCreateResumeSkillRequest, TUpdateResumeSkillRequest } from "@/shared/services/base";
import ServiceBase from "@/shared/services/ServiceBase.ts";

class ResumeSkillsService extends ServiceBase implements IResumeSkillsService {
  async createResumeSkill(params: TCreateResumeSkillRequest): Promise<IResumeSkill> {
    try {
      return await this.requestor.post("api/v1/ResumeSkills", params) as IResumeSkill;
    }
    catch (error) {
      throw error;
    }
  }

  async updateResumeSkill(params: TUpdateResumeSkillRequest): Promise<IResumeSkill> {
    try {
      return await this.requestor.put("api/v1/ResumeSkills", params) as IResumeSkill;
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
