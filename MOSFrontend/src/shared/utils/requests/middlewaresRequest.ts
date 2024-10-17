import { useUserStore } from "@/shared/stores/userStore.ts";
import { IPreparedRequest } from "@/shared/utils/requests/requestor.ts";

function authRequestMiddleware(request: IPreparedRequest) {
  const userStore = useUserStore();

  if (request.options.headers && userStore.token !== null) {
    request.options.headers["Authorization"] = userStore.token;
  }

  return request;
}

export const requestMiddlewares = [authRequestMiddleware];
