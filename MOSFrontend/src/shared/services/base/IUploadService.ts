import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IStorageInfo } from "@/shared/types";

export interface IUploadService extends IServiceBase {
  getStorageInfo(): Promise<IStorageInfo>;
}
