import { IServiceBase } from "@/shared/services/base/IServiceBase.ts";
import { useRequestor } from "@/shared/PsiUI/utils/requests/requestor.ts";

export default class ServiceBase implements IServiceBase {
  protected requestor = useRequestor();

  protected handleError(error: unknown): never {
    if (error instanceof Error) {
      throw new Error(error.message);
    }
    throw new Error("Произошла исключительная ситуация, напишите в поддержку, пожалуйста.");
  }
}
