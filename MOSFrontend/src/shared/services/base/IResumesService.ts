import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { TResume, TResumeCompact } from "@/shared/types/resume.ts";
import { AppLocale } from "@/shared/enums/common.ts";

export interface IResumesService extends IServiceBase {
  getResumesList(): Promise<TResumeCompact[]>;
  getResume(resumeId: number): Promise<TResume>;
  createResume(params: TCreateResumeRequest): Promise<TResume>;
  pinResume(resumeId: number, params: TPinResumeRequest): Promise<void>;
}

export type TCreateResumeRequest = {
  title: string;
  email: string;
  salary: number;
  currencyType: number;
};

export type TPinResumeRequest = {
  locale: AppLocale;
};
