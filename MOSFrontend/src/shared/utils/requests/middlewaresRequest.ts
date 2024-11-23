import { useUserStore } from "@/shared/stores/userStore.ts";
import { IPreparedRequest, TRequestHeaders } from "@/shared/PsiUI/utils/requests/requestor.ts";

async function authRequestMiddleware(request: IPreparedRequest): Promise<IPreparedRequest> {
  const userStore = useUserStore();

  const headers = (request.options?.headers ?? {}) as TRequestHeaders;
  headers["Locale"] = userStore.locale.toString();

  if (userStore.token !== null) {
    headers["AuthToken"] = userStore.token;
  }

  return request;
}

export const requestMiddlewares = [authRequestMiddleware];
