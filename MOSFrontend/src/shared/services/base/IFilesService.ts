import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IUploadedFilesPagination, IStorageInfo, IUploadedFile } from "@/shared/types";

export interface IFilesService extends IServiceBase {
  getStorageInfo(): Promise<IStorageInfo>;
  getFiles(payload?: TGetFilesRequest): Promise<IUploadedFilesPagination>;
  createFile(file: File): Promise<IUploadedFile>;
  deleteFile(fileId: number): Promise<void>;
}

export type TGetFilesRequest = {
  limit?: number;
  offset?: number;
};
