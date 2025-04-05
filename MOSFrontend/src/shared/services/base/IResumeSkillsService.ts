import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IResumeSkill, ResumeSkillLevelType } from "@/shared/types";

export interface IResumeSkillsService extends IServiceBase {
  createResumeSkill(params: TCreateResumeSkillRequest): Promise<IResumeSkill>;
  updateResumeSkill(params: TUpdateResumeSkillRequest): Promise<IResumeSkill>;
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
