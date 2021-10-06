#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["TrendContext.WebApi/TrendContext.WebApi.csproj", "TrendContext.WebApi/"]
COPY ["TrendContext.Domain/TrendContext.Domain.csproj", "TrendContext.Domain/"]
COPY ["TrendContext.Shared/TrendContext.Shared.csproj", "TrendContext.Shared/"]
RUN dotnet restore "TrendContext.WebApi/TrendContext.WebApi.csproj"
COPY . .
WORKDIR "/src/TrendContext.WebApi"
RUN dotnet build "TrendContext.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrendContext.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrendContext.WebApi.dll"]
EXPOSE 5000
EXPOSE 5001
