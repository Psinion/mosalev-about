# Build Stage
FROM node:lts-alpine as build

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