import {
  IFilesService
} from "@/shared/services/base";
import ServiceBase from "@/shared/services/ServiceBase.ts";
import { IStorageInfo, IUploadedFile } from "@/shared/types";

class FilesService extends ServiceBase implements IFilesService {
  async getStorageInfo(): Promise<IStorageInfo> {
    try {
      return await this.requestor.get("api/v1/files/info") as IStorageInfo;
    }
    catch (error) {
      throw error;
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
}

const FilesServiceInstance = new FilesService();

export default FilesServiceInstance;
