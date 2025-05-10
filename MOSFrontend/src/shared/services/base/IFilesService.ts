import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IFilesPagination, IStorageInfo, IUploadedFile } from "@/shared/types";

export interface IFilesService extends IServiceBase {
  getStorageInfo(): Promise<IStorageInfo>;
  getFiles(payload?: TGetFilesRequest): Promise<IFilesPagination>;
  createFile(file: File): Promise<IUploadedFile>;
}

export type TGetFilesRequest = {
  limit?: number;
  offset?: number;
};
