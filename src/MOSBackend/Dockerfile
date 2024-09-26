ARG VERSION=8.0
ARG BUILD_CONFIGURATION=Release

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build
WORKDIR /app

COPY *.sln .
COPY MOS.WebApi/*.csproj ./MOS.WebApi/
RUN dotnet restore

COPY . .
WORKDIR "./MOS.WebApi"
RUN dotnet build -c "MOS.WebApi.csproj" $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c "MOS.WebApi.csproj" $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MOS.WebApi.dll"]
