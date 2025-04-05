import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IProjectCompact } from "@/shared/types";

export interface IProjectsService extends IServiceBase {
  getProjectsList(): Promise<IProjectCompact[]>;
}
