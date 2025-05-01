import {
  IProjectsService, TCreateProjectRequest, TUpdateProjectRequest
} from "@/shared/services/base";
import { IProject, IProjectCompact } from "@/shared/types";
import ServiceBase from "@/shared/services/ServiceBase.ts";

class ProjectsService extends ServiceBase implements IProjectsService {
  async getProjectsList(): Promise<IProjectCompact[]> {
    try {
      return await this.requestor.get("api/v1/projects/list") as IProjectCompact[];
    }
    catch (error) {
      throw error;
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
}

const ProjectsServiceInstance = new ProjectsService();

export default ProjectsServiceInstance;
