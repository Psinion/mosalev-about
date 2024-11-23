import { useUserStore } from "@/shared/stores/userStore.ts";
import { IPreparedRequest, TRequestHeaders } from "@/shared/PsiUI/utils/requests/requestor.ts";

async function authRequestMiddleware(request: IPreparedRequest): Promise<IPreparedRequest> {
  const userStore = useUserStore();

  if (request.options?.headers && userStore.token !== null) {
    const headers = request.options?.headers as TRequestHeaders;
    headers["AuthToken"] = userStore.token;
    headers["Locale"] = userStore.locale.toString();
  }

  return request;
}

export const requestMiddlewares = [authRequestMiddleware];
