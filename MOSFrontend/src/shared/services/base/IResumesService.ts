import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { TResume, TResumeCompact } from "@/shared/types/resume.ts";

export interface IResumesService extends IServiceBase {
  getResumesList(): Promise<TResumeCompact[]>;
  getResume(resumeId: number): Promise<TResume>;
  createResume(params: TCreateResumeRequest): Promise<TResume>;
}

export type TCreateResumeRequest = {
  title: string;
  email: string;
  salary: number;
  currencyType: number;
};
