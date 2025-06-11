import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { IUploadedFilesPagination, IStorageInfo, IUploadedFile, FileKind } from "@/shared/types";
import { OperationResult } from "@/shared/PsiUI/utils/operationResults.ts";

export interface IFilesService extends IServiceBase {
  getStorageInfo(): Promise<IStorageInfo>;
  getFiles(payload?: TGetFilesRequest): Promise<OperationResult<IUploadedFilesPagination>>;
  createFile(file: File): Promise<IUploadedFile>;
  deleteFile(fileId: number): Promise<void>;
}

export type TGetFilesRequest = {
  limit?: number;
  offset?: number;
  fileKind?: FileKind;
};
