import { IResponseData } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { i18n } from "@/shared/utils/i18n.ts";

type TServerError = {
  statusCode: number;
  code: string;
  description: string;
  errorType: number;
};

export class ServerError extends Error {
  header: string;
  statusCode: number;
  code: string;
  errorType: number;

  constructor(title: string, statusCode: number, error?: TServerError) {
    super(error?.description ?? "");
    this.header = title;
    this.statusCode = statusCode;
    this.code = error?.code ?? "";
    this.errorType = error?.errorType ?? -1;
  }
}

export async function customErrorHandler(response: IResponseData): Promise<Error> {
  const type = response.raw.headers.get("Content-Type") || "";

  if (type.indexOf("application/json") >= 0) {
    return response.raw.json().then((x) => {
      const serverError = x as TServerError;
      const instance = new ServerError("toaster.commonErrorHeader", response.raw.status, serverError);

      if (instance.statusCode === 403) {
        instance.header = i18n.t("toaster.noPermissionsHeader");
        instance.message = i18n.t("toaster.noPermissionsDescription");
      }
      else {
        instance.header = i18n.t(instance.header);
      }

      return instance;
    });
  }

  return new ServerError(i18n.t("toaster.commonErrorHeader"), response.raw.status);
}
