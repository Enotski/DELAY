# stage1 as builder
FROM node:22.9-alpine as builder
# Copy the package.json and install dependencies
COPY package*.json ./
RUN yarn install
# Copy rest of the files
COPY . .
# Build the project
RUN yarn build


FROM nginx:alpine as production-build
COPY nginx.conf /etc/nginx/nginx.conf
## Remove default nginx index page
RUN rm -rf /usr/share/nginx/html/*
# Copy from the stage 1
COPY --from=builder /dist /usr/share/nginx/html/nested-app
EXPOSE 8043
ENTRYPOINT ["nginx", "-g", "daemon off;"]