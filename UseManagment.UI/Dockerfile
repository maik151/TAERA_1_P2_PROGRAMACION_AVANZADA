# Etapa 1: Build Node
FROM node:20 as build
WORKDIR /app

# Esto ahora funcionará porque el contexto es la carpeta UseManagment.UI
COPY package*.json ./
RUN npm install --legacy-peer-deps

COPY . .
RUN npm run build -- --configuration production

# Etapa 2: Nginx
FROM nginx:alpine
# Ajusta esta ruta si es necesario, pero esto es lo estándar
COPY --from=build /app/dist/use-managment.ui/browser /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]