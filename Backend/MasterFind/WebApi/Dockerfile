# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
# DÝKKAT: Kullanýcýyý burada deðil, en sonda ayarlayacaðýz.
# USER $APP_UID 
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Repositories/Repositories.csproj", "Repositories/"]
RUN dotnet restore "./WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# --- EKLEMEN GEREKEN KISIM BAÞLANGIÇ ---
# Root kullanýcýsý ile klasörleri oluþturuyoruz
USER root
RUN mkdir -p /app/data
RUN mkdir -p /app/uploads

# Klasörlerin sahibini uygulama kullanýcýsý ($APP_UID) yapýyoruz ki yazabilsin
# Not: $APP_UID genellikle 1654 gibi bir ID'dir. Garanti olsun diye chown yapýyoruz.
RUN chown -R $APP_UID:$APP_UID /app/data
RUN chown -R $APP_UID:$APP_UID /app/uploads

# Þimdi güvenli kullanýcýya geçiþ yapabiliriz
USER $APP_UID
# --- EKLEMEN GEREKEN KISIM BÝTÝÞ ---

ENTRYPOINT ["dotnet", "WebApi.dll"]