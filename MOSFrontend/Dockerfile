# Build Stage
FROM node:lts-alpine as build

ARG VUE_APP_API_URL
ENV VUE_APP_API_URL ${VUE_APP_API_URL}

WORKDIR /app
COPY ./package*.json .
RUN npm install

COPY ./ .
RUN npm run build

# production stage
FROM nginx:stable-alpine as production

COPY --from=build /app/dist /app
#EXPOSE 80
COPY ["./nginx.conf","/etc/nginx/nginx.conf"]