import {
  IUploadService
} from "@/shared/services/base";
import ServiceBase from "@/shared/services/ServiceBase.ts";
import { IStorageInfo } from "@/shared/types";

class UploadService extends ServiceBase implements IUploadService {
  async getStorageInfo(): Promise<IStorageInfo> {
    try {
      return await this.requestor.get("api/v1/upload/info") as IStorageInfo;
    }
    catch (error) {
      throw error;
    }
  }
}

const UploadServiceInstance = new UploadService();

export default UploadServiceInstance;
