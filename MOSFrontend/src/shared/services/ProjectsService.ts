import {
  IProjectsService, TChangeProjectVisibilityRequest, TCreateProjectRequest, TUpdateProjectRequest
} from "@/shared/services/base";
import { IProject, IProjectCompact } from "@/shared/types";
import ServiceBase from "@/shared/services/ServiceBase.ts";
import { OperationResult, Result } from "@/shared/PsiUI/utils/operationResults.ts";

class ProjectsService extends ServiceBase implements IProjectsService {
  async getProjectsList(): Promise<OperationResult<IProjectCompact[]>> {
    try {
      const result = await this.requestor.get("api/v1/projects/list") as IProjectCompact[];
      return Result.success(result);
    }
    catch (error) {
      return Result.failure(error.message);
    }
  }

  async getProject(projectId: number): Promise<IProject> {
    try {
      return await this.requestor.get(`api/v1/projects/${projectId}`) as IProjectCompact;
    }
    catch (error) {
      throw error;
    }
  }

  async createProject(payload: TCreateProjectRequest): Promise<IProject> {
    try {
      return await this.requestor.post("api/v1/projects", payload) as IProject;
    }
    catch (error) {
      throw error;
    }
  }

  async updateProject(projectId: number, payload: TUpdateProjectRequest): Promise<IProject> {
    try {
      return await this.requestor.put(`api/v1/projects/${projectId}`, payload) as IProject;
    }
    catch (error) {
      throw error;
    }
  }

  async deleteProject(projectId: number): Promise<void> {
    try {
      await this.requestor.delete(`api/v1/projects/${projectId}`);
    }
    catch (error) {
      throw error;
    }
  }

  async changeProjectVisibility(projectId: number, payload: TChangeProjectVisibilityRequest): Promise<void> {
    try {
      await this.requestor.patch(`api/v1/projects/${projectId}/visibility`, payload);
    }
    catch (error) {
      throw error;
    }
  }
}

const ProjectsServiceInstance = new ProjectsService();

export default ProjectsServiceInstance;
