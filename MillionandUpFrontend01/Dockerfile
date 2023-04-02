FROM node:16
RUN rm /etc/localtime
RUN ln -s /usr/share/zoneinfo/America/Bogota /etc/localtime
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install -g @quasar/cli
COPY . .
