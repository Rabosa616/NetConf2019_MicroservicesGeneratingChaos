FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/1.Services/Microservices.GeneratingChaos.Services.Weather/Microservices.GeneratingChaos.Services.Weather.csproj", "src/1.Services/Microservices.GeneratingChaos.Services.Weather/"]
RUN dotnet restore "src/1.Services/Microservices.GeneratingChaos.Services.Weather/Microservices.GeneratingChaos.Services.Weather.csproj"
COPY . .
WORKDIR "/src/src/1.Services/Microservices.GeneratingChaos.Services.Weather"
RUN dotnet build "Microservices.GeneratingChaos.Services.Weather.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.GeneratingChaos.Services.Weather.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.GeneratingChaos.Services.Weather.dll"]