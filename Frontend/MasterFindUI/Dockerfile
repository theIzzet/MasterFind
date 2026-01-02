# 1. Aşama: Build (Node.js)
FROM node:20-alpine as build
WORKDIR /app

# Paket listesini kopyala ve yükle
COPY package.json package-lock.json ./
RUN npm install

# Kaynak kodları kopyala
COPY . .

# Projeyi derle (dist klasörü oluşur)
RUN npm run build

# 2. Aşama: Serve (Nginx)
FROM nginx:alpine

# Derlenen React dosyalarını Nginx'in sunacağı yere kopyala
COPY --from=build /app/dist /usr/share/nginx/html

# Hazırladığımız nginx ayarını kopyala
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]