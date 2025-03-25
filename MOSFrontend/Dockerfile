# Build Stage
FROM node:lts-alpine as build

WORKDIR /app
COPY ./package*.json .
RUN npm install

COPY ./ .
RUN npm run build

# production stage
FROM nginx:stable-alpine as production

COPY --from=build /app/dist /usr/share/nginx/html
COPY ./nginx-frontend.conf /etc/nginx/conf.d/default.conf