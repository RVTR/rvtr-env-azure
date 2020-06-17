# stage - base
FROM node:lts as base
WORKDIR /workspace

COPY . .

RUN npm clean-install
RUN npm run build --prod

# stage - final
FROM nginx:mainline-alpine
LABEL maintainer="https://github.com/fredbelotte"

COPY --from=base /workspace/nginx.conf /etc/nginx/nginx.conf
COPY --from=base /workspace/dist/ /usr/share/nginx/html/

CMD ["nginx", "-g", "daemon off;"]
