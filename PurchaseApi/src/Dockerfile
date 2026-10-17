FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS projects
WORKDIR /src
COPY . .
RUN find . -type f ! -name '*.csproj' ! -name '*.props' ! -name '*.targets' ! -name 'nuget.config' -delete

FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /src
COPY --from=projects /src .
RUN dotnet restore PurchaseApi.Api/PurchaseApi/PurchaseApi.csproj

COPY . .
RUN dotnet publish PurchaseApi.Api/PurchaseApi/PurchaseApi.csproj \
    -c Release \
    -o /app/publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS final
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

COPY --from=build /app/publish .

USER $APP_UID
ENTRYPOINT ["dotnet", "PurchaseApi.dll"]