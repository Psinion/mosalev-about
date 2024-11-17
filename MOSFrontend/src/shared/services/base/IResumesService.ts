import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { TResume } from "@/shared/types/resume.ts";

export interface IResumesService extends IServiceBase {
  createResume(params: TCreateResumeRequest): Promise<TResume>;
}

export type TCreateResumeRequest = {
  title: string;
  email: string;
  salary: number;
  currencyType: number;
};
