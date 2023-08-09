#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CRM.WebUI/CRM.WebUI.csproj", "CRM.WebUI/"]
COPY ["CRM.API/CRM.API.csproj", "CRM.API/"]
COPY ["CRM.Business/CRM.Business.csproj", "CRM.Business/"]
COPY ["CRM.DataAccess/CRM.DataAccess.csproj", "CRM.DataAccess/"]
COPY ["CRM.Entity/CRM.Entity.csproj", "CRM.Entity/"]
RUN dotnet restore "CRM.WebUI/CRM.WebUI.csproj"
COPY . .
WORKDIR "/src/CRM.WebUI"
RUN dotnet build "CRM.WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CRM.WebUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CRM.WebUI.dll"]