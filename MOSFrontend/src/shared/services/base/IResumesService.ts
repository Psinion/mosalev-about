import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { TResume, TResumeCompact } from "@/shared/types/resume.ts";
import { AppLocale } from "@/shared/enums/common.ts";

export interface IResumesService extends IServiceBase {
  getResumesList(): Promise<TResumeCompact[]>;
  getResume(resumeId: number): Promise<TResume>;
  createResume(params: TCreateResumeRequest): Promise<TResume>;
  updateResume(params: TUpdateResumeRequest): Promise<TResume>;
  hasPinnedResume(): Promise<boolean>;
  getPinnedResume(): Promise<TResume>;
  pinResume(resumeId: number): Promise<void>;
  unpinResume(resumeId: number): Promise<void>;
}

export type TCreateResumeRequest = {
  title: string;
  firstName: string;
  lastName: string;
  email: string;
  salary: number;
  currencyType: number;
  about: string | null;
};

export type TUpdateResumeRequest = {
  id: number;
  title: string;
  firstName: string;
  lastName: string;
  email: string;
  salary: number;
  currencyType: number;
  about: string | null;
};

export type TPinResumeRequest = {
  locale: AppLocale;
};
