import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { TUser } from "@/shared/types";

export interface IUsersService extends IServiceBase {
  authenticate(params: TAuthenticateRequest): Promise<TAuthenticateResponse>;
  verify(): Promise<TVerifyResponse>;
}

export type TAuthenticateRequest = {
  userName: string;
  password: string;
};

export type TAuthenticateResponse = {
  user: TUser;
  token: string;
};

export type TVerifyResponse = {
  user: TUser;
};
