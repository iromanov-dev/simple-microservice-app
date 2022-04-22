#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["API/Users.API/Users.API.csproj", "API/Users.API/"]
COPY ["API/API/API.csproj", "API/API/"]
COPY ["Core/Core.Users/Core.Users.csproj", "Core/Core.Users/"]
COPY ["Core/Core.Common/Core.Common.csproj", "Core/Core.Common/"]
RUN dotnet restore "API/Users.API/Users.API.csproj"
COPY . .
WORKDIR "/src/API/Users.API"
RUN dotnet build "Users.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Users.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.API.dll"]
