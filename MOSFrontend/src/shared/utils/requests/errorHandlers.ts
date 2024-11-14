import { IResponseData } from "@/shared/utils/requests/requestor.ts";

type TServerError = {
  statusCode: number;
  code: string;
  description: string;
  errorType: number;
};

export class ServerError extends Error {
  statusCode: number;
  code: string;
  errorType: number;

  constructor(statusCode: number, error: TServerError) {
    super(error.description);
    this.statusCode = statusCode;
    this.code = error.code;
    this.errorType = error.errorType;
  }
}

export async function customErrorHandler(response: IResponseData): Promise<Error> {
  const type = response.raw.headers.get("Content-Type") || "";

  if (type.indexOf("application/json") >= 0) {
    return response.raw.json().then((x) => {
      const serverError = x as TServerError;
      return new ServerError(response.raw.status, serverError);
    });
  }

  return new Error(response.raw.statusText);
}
