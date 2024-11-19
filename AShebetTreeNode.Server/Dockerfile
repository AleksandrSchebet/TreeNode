FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs

FROM node AS build
WORKDIR /src
COPY ["ashebettreenode.client/nuget.config", "ashebettreenode.client/"]
COPY ["AShebetTreeNode.Server/AShebetTreeNode.Server.csproj", "AShebetTreeNode.Server/"]
COPY ["ashebettreenode.client/ashebettreenode.client.esproj", "ashebettreenode.client/"]
RUN dotnet restore "./AShebetTreeNode.Server/AShebetTreeNode.Server.csproj"
COPY . .
WORKDIR "/src/AShebetTreeNode.Server"
RUN dotnet build "./AShebetTreeNode.Server.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AShebetTreeNode.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AShebetTreeNode.Server.dll"]