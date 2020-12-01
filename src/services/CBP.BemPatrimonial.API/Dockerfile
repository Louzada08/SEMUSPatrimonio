#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/services/CBP.BemPatrimonial.API/CBP.BemPatrimonial.API.csproj", "src/services/CBP.BemPatrimonial.API/"]
RUN dotnet restore "src/services/CBP.BemPatrimonial.API/CBP.BemPatrimonial.API.csproj"
COPY . .
WORKDIR "/src/src/services/CBP.BemPatrimonial.API"
RUN dotnet build "CBP.BemPatrimonial.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CBP.BemPatrimonial.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CBP.BemPatrimonial.API.dll"]