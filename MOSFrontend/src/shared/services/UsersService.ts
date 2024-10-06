import { IUsersService, TAuthenticateRequest, TAuthenticateResponse } from "@/shared/services/base";
import { useRequestor } from "@/shared/utils/requests/requestor.ts";

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
}

const UsersServiceInstance = new UsersService();

export default UsersServiceInstance;
