import { IUsersService, TAuthenticateRequest, TAuthenticateResponse, TVerifyResponse } from "@/shared/services/base";
import ServiceBase from "@/shared/services/ServiceBase.ts";

class UsersService extends ServiceBase implements IUsersService {
  async authenticate(params: TAuthenticateRequest): Promise<TAuthenticateResponse> {
    try {
      return await this.requestor.post("api/v1/users/authenticate", params) as TAuthenticateResponse;
    }
    catch (error) {
      throw error;
    }
  }

  async verify(): Promise<TVerifyResponse> {
    try {
      return await this.requestor.get("api/v1/users/verify") as TVerifyResponse;
    }
    catch (error) {
      throw error;
    }
  }
}

const UsersServiceInstance = new UsersService();

export default UsersServiceInstance;
