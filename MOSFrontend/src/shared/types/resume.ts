import { AppLocale } from "@/shared/enums/common.ts";
import { TUser } from "@/shared/types/user.ts";

export type TResume = {
  id: number;
  title: string;
  firstName: string;
  lastName: string;
  email: string;
  salary: number;
  about: string | null;
  currencyType: CurrencyType;
  dateCreate: string | null;
  dateUpdate: string | null;

  companyEntries: TResumeCompanyEntry[];
  skills: ResumeSkill[];
};

export type TResumeCompact = {
  id: number;
  title: string;
  pinnedToLocale: AppLocale | null;
  dateCreate: string | null;
  userCreate: TUser | null;
  dateUpdate: string | null;
  userUpdate: TUser | null;
};

export enum CurrencyType {
  Ruble = 1,
  Dollar,
  Euro
}

export type TResumeCompanyEntry = {
  id: number;
  resumeId: number;
  company: string;
  webSiteUrl: string | null;
  description: string | null;
  resumePosts: ResumeCompanyEntryPost[];
};

export interface ResumeCompanyEntryPost {
  id: number;
  resumeCompanyEntryId: number;
  name?: string;
  description?: string | null;
  dateStart?: string;
  dateEnd?: string | null;
}

export interface ResumeSkill {
  id: number;
  resumeId: number;
  name: string;
  level: ResumeSkillLevelType;
}

export enum ResumeSkillLevelType {
  Low = 0,
  Medium,
  High
}
