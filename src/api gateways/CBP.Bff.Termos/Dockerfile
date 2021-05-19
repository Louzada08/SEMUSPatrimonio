#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/api gateways/CBP.Bff.Termos/CBP.Bff.Termos.csproj", "src/api gateways/CBP.Bff.Termos/"]
RUN dotnet restore "src/api gateways/CBP.Bff.Termos/CBP.Bff.Termos.csproj"
COPY . .
WORKDIR "/src/src/api gateways/CBP.Bff.Termos"
RUN dotnet build "CBP.Bff.Termos.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CBP.Bff.Termos.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CBP.Bff.Termos.dll"]