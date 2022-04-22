#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["API/Organizations.API/Organizations.API.csproj", "API/Organizations.API/"]
COPY ["Data/Data.csproj", "Data/"]
COPY ["API/API/API.csproj", "API/API/"]
COPY ["Core/Core.Organizations/Core.Organizations.csproj", "Core/Core.Organizations/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
RUN dotnet restore "API/Organizations.API/Organizations.API.csproj"
COPY . .
WORKDIR "/src/API/Organizations.API"
RUN dotnet build "Organizations.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Organizations.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Organizations.API.dll"]