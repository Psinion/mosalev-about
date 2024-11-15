import { createRouter, createWebHistory } from "vue-router";
import { protectedMainRoutes, publicMainRoutes } from "../modules/main/routes.ts";
import { resumeRoutes } from "@/modules/resume/routes.ts";
import { protectedPagesGuard } from "@/router/middlewares/protectedPagesGuard.ts";
import { errorRoutes } from "@/modules/errors/routes.ts";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [{
    path: "/",
    beforeEnter: protectedPagesGuard,
    children: [
      ...protectedMainRoutes
    ]
  },
  ...publicMainRoutes,
  ...resumeRoutes,
  ...errorRoutes,
  {
    path: "/:catchAll(.*)",
    redirect: "/error/404"
  }
  ]
});

export default router;
