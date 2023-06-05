#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/InterfaceAdapter/Wsa.Gaas.GobbletGobblers.Core.WebApi/Wsa.Gaas.GobbletGobblers.Core.WebApi.csproj", "src/InterfaceAdapter/Wsa.Gaas.GobbletGobblers.Core.WebApi/"]
COPY ["src/Application/Wsa.Gaas.GobbletGobblers.Application/Wsa.Gaas.GobbletGobblers.Application.csproj", "src/Application/Wsa.Gaas.GobbletGobblers.Application/"]
COPY ["src/Domain/Wsa.Gaas.GobbletGobblers.Domain/Wsa.Gaas.GobbletGobblers.Domain.csproj", "src/Domain/Wsa.Gaas.GobbletGobblers.Domain/"]
RUN dotnet restore "src/InterfaceAdapter/Wsa.Gaas.GobbletGobblers.Core.WebApi/Wsa.Gaas.GobbletGobblers.Core.WebApi.csproj"
COPY . .
WORKDIR "/src/src/InterfaceAdapter/Wsa.Gaas.GobbletGobblers.Core.WebApi"
RUN dotnet build "Wsa.Gaas.GobbletGobblers.Core.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Wsa.Gaas.GobbletGobblers.Core.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wsa.Gaas.GobbletGobblers.Core.WebApi.dll"]