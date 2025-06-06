import { IResumeCompanyEntryPost } from "@/shared/types";
import { IServiceBase } from "./IServiceBase";

export interface IResumePostsService extends IServiceBase {
  createResumePost(params: TCreateResumePostRequest): Promise<IResumeCompanyEntryPost>;
  updateResumePost(params: TUpdateResumePostRequest): Promise<IResumeCompanyEntryPost>;
  deleteResumePost(postId: number): Promise<boolean>;
}

export type TCreateResumePostRequest = {
  resumeCompanyEntryId: number;
  name: string;
  description?: string;
  dateStart?: string;
  dateEnd?: string;
};

export type TUpdateResumePostRequest = {
  id: number;
  name: string;
  description?: string;
  dateStart?: string;
  dateEnd?: string;
};
