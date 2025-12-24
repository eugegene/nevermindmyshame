FROM mcr.microsoft.com/dotnet/aspnet:10.0.0-alpine3.22 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
ARG PROJ_PATH="./track-list-api.csproj"
WORKDIR "/src/."
COPY "track-list-api.csproj" "."
RUN dotnet restore $PROJ_PATH 

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

COPY . .
WORKDIR "/src/."
RUN dotnet build $PROJ_PATH -c $BUILD_CONFIGURATION -o /app/build

# Створюємо самодостатній файл міграцій (Bundle)
RUN dotnet ef migrations bundle --self-contained -r linux-musl-x64 -o /app/publish/efbundle

FROM build AS publish
RUN dotnet publish $PROJ_PATH -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
USER root
COPY --from=publish /app/publish .

COPY entrypoint.sh /
#RUN apk add --no-cache netcat-openbsd
RUN chmod +x "/entrypoint.sh"
RUN chmod +x efbundle

ENTRYPOINT ["/bin/sh", "/entrypoint.sh"]