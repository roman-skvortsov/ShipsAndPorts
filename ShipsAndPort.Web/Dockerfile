#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ShipsAndPort.Web/ShipsAndPorts.Web.csproj", "ShipsAndPort.Web/"]
COPY ["ShipsAndPorts.Core/ShipsAndPorts.Core.csproj", "ShipsAndPorts.Core/"]
COPY ["ShipsAndPorts.Services/ShipsAndPorts.Services.csproj", "ShipsAndPorts.Services/"]
COPY ["ShipsAndPorts.Repositories/ShipsAndPorts.Repositories.csproj", "ShipsAndPorts.Repositories/"]
RUN dotnet restore "ShipsAndPort.Web/ShipsAndPorts.Web.csproj"
COPY . .
WORKDIR "/src/ShipsAndPort.Web"
RUN dotnet build "ShipsAndPorts.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShipsAndPorts.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipsAndPorts.Web.dll"]