#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Notion.Api/Notion.Api.csproj", "Notion.Api/"]
COPY ["Notion.Comman/Notion.Comman.csproj", "Notion.Comman/"]
COPY ["Notion.Services/Notion.Services.csproj", "Notion.Services/"]
COPY ["Notion.DAL/Notion.DAL.csproj", "Notion.DAL/"]
RUN dotnet restore "Notion.Api/Notion.Api.csproj"
COPY . .
WORKDIR "/src/Notion.Api"
RUN dotnet build "Notion.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notion.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notion.Api.dll"]