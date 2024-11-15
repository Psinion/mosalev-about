import { NavigationGuardNext, RouteLocationNormalized } from "vue-router";
import { useUserStore } from "@/shared/stores/userStore.ts";
import { RouteNames } from "@/router/routeNames.ts";

export async function protectedPagesGuard(
  to: RouteLocationNormalized,
  from: RouteLocationNormalized,
  next: NavigationGuardNext
): Promise<void> {
  const userStore = useUserStore();

  if (userStore.token === null) {
    next({ name: RouteNames.Error404 });
    return;
  }

  next();
}
