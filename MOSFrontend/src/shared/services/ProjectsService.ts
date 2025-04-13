import {
  IProjectsService, TCreateProjectRequest
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

  async createProject(payload: TCreateProjectRequest): Promise<IProject> {
    try {
      return await this.requestor.post("api/v1/projects", payload) as IProject;
    }
    catch (error) {
      throw error;
    }
  }
}

const ProjectsServiceInstance = new ProjectsService();

export default ProjectsServiceInstance;
