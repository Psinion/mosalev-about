import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IStorageInfo, IUploadedFile } from "@/shared/types";

export interface IFilesService extends IServiceBase {
  getStorageInfo(): Promise<IStorageInfo>;
  createFile(file: File): Promise<IUploadedFile>;
}
