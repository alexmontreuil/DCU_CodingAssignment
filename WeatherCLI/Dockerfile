# weathercli/Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY *.sln .
COPY WeatherCLI/*.csproj ./weathercli/
RUN dotnet restore

COPY WeatherCLI/. ./WeatherCLI/
WORKDIR /src/WeatherCLI
RUN dotnet publish -c Release -o /app

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "WeatherCLI.dll"]
