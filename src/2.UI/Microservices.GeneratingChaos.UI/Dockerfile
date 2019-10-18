FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/2.UI/Microservices.GeneratingChaos.UI/Microservices.GeneratingChaos.UI.csproj", "src/2.UI/Microservices.GeneratingChaos.UI/"]
RUN dotnet restore "src/2.UI/Microservices.GeneratingChaos.UI/Microservices.GeneratingChaos.UI.csproj"
COPY . .
WORKDIR "/src/src/2.UI/Microservices.GeneratingChaos.UI"
RUN dotnet build "Microservices.GeneratingChaos.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservices.GeneratingChaos.UI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservices.GeneratingChaos.UI.dll"]