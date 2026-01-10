# Etapa 1: Build Node
FROM node:20 as build
WORKDIR /app

COPY package*.json ./
RUN npm install --legacy-peer-deps

COPY . .
RUN npm run build -- --configuration production

# Etapa 2: Nginx
FROM nginx:alpine


COPY --from=build /app/dist/UseManagment.UI/browser /usr/share/nginx/html

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]