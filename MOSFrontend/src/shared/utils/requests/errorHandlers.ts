import { IResponseData } from "@/shared/utils/requests/requestor.ts";

type TServerError = {
  code: string;
  description: string;
  errorType: number;
};

export class ServerError extends Error {
  code: string;
  errorType: number;

  constructor(error: TServerError) {
    super(error.description);
    this.code = error.code;
    this.errorType = error.errorType;
  }
}

export async function customErrorHandler(response: IResponseData): Promise<Error> {
  const type = response.raw.headers.get("Content-Type") || "";

  if (type.indexOf("application/json") >= 0) {
    return response.raw.json().then((x) => {
      const serverError = x as TServerError;
      return new ServerError(serverError);
    });
  }

  return new Error(response.raw.statusText);
}
