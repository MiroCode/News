#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/NewsApp.Api/NewsApp.Api.csproj", "src/NewsApp.Api/"]
COPY ["src/NewsApp.Infrastructure/NewsApp.Infrastructure.csproj", "src/NewsApp.Infrastructure/"]
COPY ["src/NewsApp.Manager/NewsApp.Manager.csproj", "src/NewsApp.Manager/"]
RUN dotnet restore "src/NewsApp.Api/NewsApp.Api.csproj"
COPY . .
WORKDIR "/src/src/NewsApp.Api"
RUN dotnet build "NewsApp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NewsApp.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NewsApp.Api.dll"]
