FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/1.Services/Microservices.GeneratingChaos.Services.Api/Microservices.GeneratingChaos.Services.Api.csproj", "src/1.Services/Microservices.GeneratingChaos.Services.Api/"]
RUN dotnet restore "src/1.Services/Microservices.GeneratingChaos.Services.Api/Microservices.GeneratingChaos.Services.Api.csproj"
COPY . .
WORKDIR "/src/src/1.Services/Microservices.GeneratingChaos.Services.Api"
RUN dotnet build "Microservices.GeneratingChaos.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.GeneratingChaos.Services.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.GeneratingChaos.Services.Api.dll"]