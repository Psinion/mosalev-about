import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IProject, IProjectCompact } from "@/shared/types";

export interface IProjectsService extends IServiceBase {
  getProjectsList(): Promise<IProjectCompact[]>;
  createProject(payload: TCreateProjectRequest): Promise<IProject>;
}

export type TCreateProjectRequest = {
  title: string;
  description: string;
};
