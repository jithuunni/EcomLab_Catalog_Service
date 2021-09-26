#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["EcomLab.CatalogService.Api/EcomLab.CatalogService.Api.csproj", "EcomLab.CatalogService.Api/"]
RUN dotnet restore "EcomLab.CatalogService.Api/EcomLab.CatalogService.Api.csproj"
COPY . .
WORKDIR "/src/EcomLab.CatalogService.Api"
RUN dotnet build "EcomLab.CatalogService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcomLab.CatalogService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcomLab.CatalogService.Api.dll"]
