import type { App } from "vue";

interface Config {
  appUrl: string;
}

export const mainConfig: Config = {
  appUrl: import.meta.env.VITE_APP_URL as string
};

export default {
  install: (Vue: App) => {
    Vue.config.globalProperties.mainConfig = mainConfig;
  }
};

// Declaration for typescript intellisense.
declare module "@vue/runtime-core" {
  interface ComponentCustomProperties {
    mainConfig: Config;
  }
}
