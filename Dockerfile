﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProyectoDAWBackend/ProyectoDAWBackend.csproj", "ProyectoDAWBackend/"]
RUN dotnet restore "ProyectoDAWBackend/ProyectoDAWBackend.csproj"
COPY . .
WORKDIR "/src/ProyectoDAWBackend"
RUN dotnet build "ProyectoDAWBackend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProyectoDAWBackend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProyectoDAWBackend.dll"]