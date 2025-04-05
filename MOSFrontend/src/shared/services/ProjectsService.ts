import {
  IProjectsService
} from "@/shared/services/base";
import { useRequestor } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { IProjectCompact } from "@/shared/types";

class ProjectsService implements IProjectsService {
  private requestor = useRequestor();

  async getProjectsList(): Promise<IProjectCompact[]> {
    try {
      return await this.requestor.get("api/v1/projects/list") as IProjectCompact[];
    }
    catch (error) {
      throw error;
    }
  }
}

const ProjectsServiceInstance = new ProjectsService();

export default ProjectsServiceInstance;
