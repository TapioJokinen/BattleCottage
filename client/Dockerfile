FROM node:20-alpine3.17 AS build

WORKDIR /app

COPY package*.json ./

RUN npm ci

COPY . .

RUN npm run build


FROM node:20-alpine3.17 AS runner

WORKDIR /app

COPY --from=build /app/package*.json ./
COPY --from=build /app/.next ./.next
COPY --from=build /app/node_modules ./node_modules

ENV NODE_ENV=production

CMD ["node", ".next/standalone/server.js"]