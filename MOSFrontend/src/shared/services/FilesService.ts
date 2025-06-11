import {
  IFilesService, TGetFilesRequest
} from "@/shared/services/base";
import ServiceBase from "@/shared/services/ServiceBase.ts";
import { IUploadedFilesPagination, IStorageInfo, IUploadedFile } from "@/shared/types";
import { OperationResult, Result } from "@/shared/PsiUI/utils/operationResults.ts";

class FilesService extends ServiceBase implements IFilesService {
  async getStorageInfo(): Promise<IStorageInfo> {
    try {
      return await this.requestor.get("api/v1/files/info") as IStorageInfo;
    }
    catch (error) {
      throw error;
    }
  }

  async getFiles(payload?: TGetFilesRequest): Promise<OperationResult<IUploadedFilesPagination>> {
    try {
      const result = await this.requestor.get("api/v1/files/list", payload) as IUploadedFilesPagination;
      return Result.success(result);
    }
    catch (error) {
      return Result.failure(error.message);
    }
  }

  async createFile(file: File): Promise<IUploadedFile> {
    try {
      const formData = new FormData();
      formData.append("file", file);
      return await this.requestor.post("api/v1/files", formData) as IUploadedFile;
    }
    catch (error) {
      throw error;
    }
  }

  async deleteFile(fileId: number): Promise<void> {
    try {
      await this.requestor.delete(`api/v1/files/${fileId}`);
    }
    catch (error) {
      throw error;
    }
  }
}

const FilesServiceInstance = new FilesService();

export default FilesServiceInstance;
