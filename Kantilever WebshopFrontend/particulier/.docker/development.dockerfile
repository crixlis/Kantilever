FROM node

ENV HOME=/usr/src/app
WORKDIR $HOME

ENV APP_NAME=particulier

RUN npm install -g angular-cli
COPY . $HOME
EXPOSE 69:4200
EXPOSE 49153

