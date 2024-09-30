ARG VERSION=8.0
ARG BUILD_CONFIGURATION=Release

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build
WORKDIR /app

COPY *.sln .
COPY MOS.WebApi/*.csproj ./MOS.WebApi/
COPY MOS.Data.EF.Access/*.csproj ./MOS.Data.EF.Access/
COPY MOS.Domain/*.csproj ./MOS.Domain/
COPY MOS.Application/*.csproj ./MOS.Application/
RUN dotnet restore

COPY . .
WORKDIR "./MOS.WebApi"
RUN dotnet build -c "MOS.WebApi.csproj" $BUILD_CONFIGURATION -o /app/build

RUN dotnet publish -c "MOS.WebApi.csproj" $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:$VERSION AS base
WORKDIR /app

#ENV ASPNETCORE_URLS=https://+:8081;http://+:8080
#ENV ASPNETCORE_HTTPS_PORT=8081
EXPOSE 8080
EXPOSE 8081

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "MOS.WebApi.dll"]
