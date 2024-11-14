import { useUserStore } from "@/shared/stores/userStore.ts";
import { IPreparedRequest, TRequestHeaders } from "@/shared/utils/requests/requestor.ts";

async function authRequestMiddleware(request: IPreparedRequest): Promise<IPreparedRequest> {
  const userStore = useUserStore();

  if (request.options?.headers && userStore.token !== null) {
    const headers = request.options?.headers as TRequestHeaders;
    headers["AuthToken"] = userStore.token;
  }

  return request;
}

export const requestMiddlewares = [authRequestMiddleware];
