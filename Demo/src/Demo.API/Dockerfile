#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/Demo.API/Demo.API.csproj", "src/Demo.API/"]
COPY ["src/Demo.Infra/Demo.Infra.csproj", "src/Demo.Infra/"]
COPY ["src/Demo.Domain/Demo.Domain.csproj", "src/Demo.Domain/"]
COPY ["src/Demo.Business/Demo.Business.csproj", "src/Demo.Business/"]
RUN dotnet restore "src/Demo.API/Demo.API.csproj"
COPY . .
WORKDIR "/src/src/Demo.API"
RUN dotnet build "Demo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Demo.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Demo.API.dll"]