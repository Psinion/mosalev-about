import { IUsersService, TAuthenticateRequest, TAuthenticateResponse, TVerifyResponse } from "@/shared/services/base";
import { useRequestor } from "@/shared/PsiUI/utils/requests/requestor.ts";

class UsersService implements IUsersService {
  private requestor = useRequestor();

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
