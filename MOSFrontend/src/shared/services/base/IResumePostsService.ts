import { ResumeCompanyEntryPost } from "@/shared/types";
import { IServiceBase } from "./IServiceBase";

export interface IResumePostsService extends IServiceBase {
  createResumePost(params: TCreateResumePostRequest): Promise<ResumeCompanyEntryPost>;
  updateResumePost(params: TUpdateResumePostRequest): Promise<ResumeCompanyEntryPost>;
  deleteResumePost(postId: number): Promise<boolean>;
}

export type TCreateResumePostRequest = {
  resumeCompanyEntryId: number;
  name: string;
  description?: string;
  dateStart?: string | Date;
  dateEnd?: string | Date;
};

export type TUpdateResumePostRequest = {
  id: number;
  name: string;
  description?: string;
  dateStart?: string | Date;
  dateEnd?: string | Date;
};
