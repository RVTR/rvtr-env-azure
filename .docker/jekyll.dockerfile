# stage - base
FROM ruby:2.6 as base

WORKDIR /workspace
COPY . .

RUN bundle install
RUN bundle exec jekyll build --config jekyll.config.yaml

# stage - final
FROM nginx:stable-alpine

LABEL maintainer="https://github.com/fredbelotte"

COPY --from=base /workspace/_site /usr/share/nginx/html

CMD ["nginx", "-g", "daemon off;"]
