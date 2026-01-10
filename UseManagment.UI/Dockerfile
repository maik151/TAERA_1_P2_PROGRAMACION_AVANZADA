# Etapa 1: Construcción (Node.js)
FROM node:20 as build
WORKDIR /app

# Copiamos package.json primero para aprovechar caché
COPY package*.json ./
RUN npm install --legacy-peer-deps

# Copiamos todo el código fuente
COPY . .
# Construimos para producción
RUN npm run build -- --configuration production

# Etapa 2: Servidor Web (Nginx)
FROM nginx:alpine
# ¡OJO AQUÍ! Revisa que la ruta coincida con tu angular.json
# Angular 17+ suele crear la carpeta en dist/nombre-proyecto/browser
# Si tu proyecto se llama 'use-managment.ui', la ruta abajo debería ser correcta:
COPY --from=build /app/dist/use-managment.ui/browser /usr/share/nginx/html

# Exponer el puerto 80
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]