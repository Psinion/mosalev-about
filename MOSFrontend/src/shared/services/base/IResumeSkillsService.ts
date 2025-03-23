import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { ResumeSkill, ResumeSkillLevelType } from "@/shared/types";

export interface IResumeSkillsService extends IServiceBase {
  createResumeSkill(params: TCreateResumeSkillRequest): Promise<ResumeSkill>;
  updateResumeSkill(params: TUpdateResumeSkillRequest): Promise<ResumeSkill>;
  deleteResumeSkill(skillId: number): Promise<boolean>;
}

export type TCreateResumeSkillRequest = {
  resumeId: number;
  name: string;
  level: ResumeSkillLevelType;
};

export type TUpdateResumeSkillRequest = {
  id: number;
  name: string;
  level: ResumeSkillLevelType;
};
