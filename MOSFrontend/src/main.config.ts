import type { App } from "vue";
import { version } from "../package.json";

interface Config {
  appUrl: string;
  version: string;
}

export const mainConfig: Config = {
  appUrl: import.meta.env.VITE_APP_URL as string,
  version: version as string
};

export const MainConfigPlugin = {
  install: (Vue: App) => {
    Vue.config.globalProperties.$mainConfig = mainConfig;
  }
};

// Declaration for typescript intellisense.
declare module "@vue/runtime-core" {
  interface ComponentCustomProperties {
    $mainConfig: Config;
  }
}
