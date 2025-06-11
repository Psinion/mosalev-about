import { IResponseData } from "@/shared/PsiUI/utils/requests/requestor.ts";
import { i18n } from "@/setup/i18n.ts";
import { ServerError, TServerError } from "@/shared/utils/requests/errors.ts";

export async function customErrorHandler(response: IResponseData): Promise<Error> {
  const type = response.raw.headers.get("Content-Type") || "";

  if (type.indexOf("application/json") >= 0) {
    return response.raw.json().then((x) => {
      const serverError = x as TServerError;
      const instance = new ServerError(response.raw.status, serverError);

      if (instance.statusCode === 403) {
        instance.message = i18n.t("toaster.noPermissionsDescription");
      }

      return instance;
    });
  }

  return new ServerError(response.raw.status);
}
