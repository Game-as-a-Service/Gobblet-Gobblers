#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/InterfaceAdapter/Gaas.GobbletGobblers.Core.WebApi/Gaas.GobbletGobblers.Core.WebApi.csproj", "src/InterfaceAdapter/Gaas.GobbletGobblers.Core.WebApi/"]
COPY ["/Application/Gaas.GobbletGobblers.Application/Gaas.GobbletGobblers.Application.csproj", "src/Application/Gaas.GobbletGobblers.Application/"]
COPY ["/Domain/Gaas.GobbletGobblers.Domain/Gaas.GobbletGobblers.Domain.csproj", "src/Domain/Gaas.GobbletGobblers.Domain/"]
RUN dotnet restore "./src/InterfaceAdapter/Gaas.GobbletGobblers.Core.WebApi/Gaas.GobbletGobblers.Core.WebApi.csproj"
COPY . .
WORKDIR "/src/InterfaceAdapter/Gaas.GobbletGobblers.Core.WebApi"
RUN dotnet build "Gaas.GobbletGobblers.Core.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gaas.GobbletGobblers.Core.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gaas.GobbletGobblers.Core.WebApi.dll"]