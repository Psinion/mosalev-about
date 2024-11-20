import {
  IResponseData
} from "@/shared/PsiUI/utils/requests/requestor.ts";

async function defaultResponseMiddleware(response: IResponseData): Promise<IResponseData> {
  return response;
}

export const responseMiddlewares = [defaultResponseMiddleware];
