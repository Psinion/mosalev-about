import { createRouter, createWebHistory } from "vue-router";
import { mainRoutes } from "../modules/main/routes.ts";

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [{
    path: "/",
    children: [
      ...mainRoutes
    ]
  }]
});

export default router;
