FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR "/app"
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION="Release"
WORKDIR "/src"
COPY ["NokoWebApi/NokoWebApi.csproj", "NokoWebApi/"]
COPY ["NokoWebApiSdk/NokoWebApiSdk.csproj", "NokoWebApiSdk/"]
RUN dotnet restore "NokoWebApi/NokoWebApi.csproj"
RUN dotnet restore "NokoWebApiSdk/NokoWebApiSdk.csproj"
COPY "NokoWebApi" .
COPY "NokoWebApiSdk" .
WORKDIR "/src/NokoWebApi"
RUN dotnet build "NokoWebApi.csproj" -c $BUILD_CONFIGURATION -o "/app/build"
WORKDIR "/src/NokoWebApiSdk"
RUN dotnet build "NokoWebApiSdk.csproj" -c $BUILD_CONFIGURATION -o "/app/build"

FROM build AS publish
ARG BUILD_CONFIGURATION="Release"
WORKDIR "/src/NokoWebApi"
RUN dotnet publish "NokoWebApi.csproj" -c $BUILD_CONFIGURATION -o "/app/publish" /p:UseAppHost=false
WORKDIR "/src/NokoWebApiSdk"
RUN dotnet publish "NokoWebApiSdk.csproj" -c $BUILD_CONFIGURATION -o "/app/publish" /p:UseAppHost=false

FROM base AS final
WORKDIR "/app"
COPY --from=publish "/app/publish" .
ENTRYPOINT ["dotnet", "NokoWebApi.dll"]
