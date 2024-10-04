import { createApp } from 'vue';
import App from './App.vue';
import router from "./router";
import {createPinia} from "pinia";
import ElementPlus from 'element-plus';
import '@/assets/styles/index.scss';

const app = createApp(App);

app.use(router);
app.use(createPinia);
app.use(ElementPlus);

app.mount('#app');