FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /src
COPY ./src ./

RUN dotnet restore BattleCottage.sln

WORKDIR /src/BattleCottage.Web

# Restore project
RUN dotnet restore BattleCottage.Web.csproj

# Build project
RUN dotnet build BattleCottage.Web.csproj -c Release

# Publish project
RUN dotnet publish BattleCottage.Web.csproj -c Release -o /app/published

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS runtime

WORKDIR /app

COPY --from=build /app/published ./

CMD ["dotnet", "BattleCottage.Web.dll"]
