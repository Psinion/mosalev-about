import { createRouter, createWebHistory } from "vue-router";
import { protectedMainRoutes, publicMainRoutes } from "../modules/main/routes.ts";
import { protectedPagesGuard } from "@/router/middlewares/protectedPagesGuard.ts";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [{
    path: "/",
    beforeEnter: protectedPagesGuard,
    children: [
      ...protectedMainRoutes
    ]
  },
  ...publicMainRoutes]
});

export default router;
