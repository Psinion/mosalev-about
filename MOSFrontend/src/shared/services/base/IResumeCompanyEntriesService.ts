import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { TResumeCompanyEntry } from "@/shared/types/resume.ts";

export interface IResumeCompanyEntriesService extends IServiceBase {
  createResumeCompanyEntry(params: TCreateResumeCompanyEntryRequest): Promise<TResumeCompanyEntry>;
  updateResumeCompanyEntry(params: TUpdateResumeCompanyEntryRequest): Promise<TResumeCompanyEntry>;
  deleteResumeCompanyEntry(companyId: number): Promise<boolean>;
}

export type TCreateResumeCompanyEntryRequest = {
  resumeId: number;
  company: string;
  webSiteUrl?: string | null;
  description?: string | null;
};

export type TUpdateResumeCompanyEntryRequest = {
  id: number;
  company: string;
  webSiteUrl?: string | null;
  description?: string | null;
};
