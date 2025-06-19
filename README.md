# DCU_CodingAssignment WeatherAPI + WeatherCLI
An ASP.NET Core Web API and CLI application that integrates with the OpenWeatherMap API to provide current and average weather forecasts by ZIP code.

## Project Structure

├── WeatherAPI/        # ASP.NET Core Web API

├── weathercli/            # .NET CLI for weather access

├── WeatherService.Tests/  # Unit tests for Web API

├── weathercli.Tests/      # Unit tests for CLI

├── WeatherService.sln     # Solution file

## Features
✅ WeatherService API
GET /weather/current/{zipcode}?units=fahrenheit|celsius

GET /weather/average/{zipcode}?units=fahrenheit|celsius&timePeriod=2-5

Input validation and proper status codes

Optional API key authentication

✅ CLI: weathercli
get-current-weather <zipcode> <units> [--output json|yaml|text]

get-average-weather <zipcode> <units> <days> [--output json|yaml|text|table]


## Prerequisites
.NET 8 SDK

Docker Desktop (optional)

An OpenWeatherMap API key

## Setup & Run Locally on windows
### Web API
open DOS windows
cd WeatherAPI (project root directory)
dotnet run
Example:
http://localhost:5000/weather/current/10001?units=fahrenheit


## Docker Instructions
### Build and Run API (Windows)
open DOS windows
cd WeatherAPI (project root directory)
docker build -t weather-api -f Dockerfile .
docker run -d -p 8080:80 --name weatherapi weather-api
Visit: http://localhost:8080/weather/current/10001?units=fahrenheit



## Output Formats (CLI)
JSON
yaml
text
table

🗂 Sample CLI Usage
open DOS windows
cd WeatherCLI (project root directory)

### Current weather
dotnet run -- get-current-weather 10001 celsius --baseurl http://localhost:5257

### Average weather
dotnet run -- get-average-weather 10001 fahrenheit 3 --output table --baseurl http://localhost:5257


## Deliverables
 ASP.NET Core Web API

 CLI with formatting options

 Docker support

 Unit tests (API and CLI)

 Mocked HTTP dependencies

 Documentation with setup & usage



