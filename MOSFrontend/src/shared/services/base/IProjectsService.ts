import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IProject, IProjectCompact } from "@/shared/types";
import { OperationResult } from "@/shared/PsiUI/utils/operationResults.ts";

export interface IProjectsService extends IServiceBase {
  getProjectsList(): Promise<OperationResult<IProjectCompact[]>>;
  getProject(projectId: number): Promise<IProject>;
  createProject(payload: TCreateProjectRequest): Promise<IProject>;
  updateProject(projectId: number, payload: TUpdateProjectRequest): Promise<IProject>;
  deleteProject(projectId: number): Promise<void>;
  changeProjectVisibility(projectId: number, payload: TChangeProjectVisibilityRequest): Promise<void>;
}

export type TCreateProjectRequest = {
  title: string;
  description: string;
};

export type TUpdateProjectRequest = {
  title: string;
  description: string;
};

export type TChangeProjectVisibilityRequest = {
  visible: boolean;
};
