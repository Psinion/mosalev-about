import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IProject, IProjectCompact } from "@/shared/types";

export interface IProjectsService extends IServiceBase {
  getProjectsList(): Promise<IProjectCompact[]>;
  getProject(projectId: number): Promise<IProject>;
  createProject(payload: TCreateProjectRequest): Promise<IProject>;
  updateProject(projectId: number, payload: TUpdateProjectRequest): Promise<IProject>;
  deleteProject(projectId: number): Promise<void>;
}

export type TCreateProjectRequest = {
  title: string;
  description: string;
};

export type TUpdateProjectRequest = {
  title: string;
  description: string;
};
