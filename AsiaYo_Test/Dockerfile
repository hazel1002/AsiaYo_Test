# Use the official .NET Core SDK as a parent image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore any dependencies (use .csproj for the project name)
COPY AsiaYo_Test.csproj ./
RUN dotnet restore #下載安裝相依的NuGet套件

COPY . .

# Publish the application
RUN dotnet publish -c Release -o out

# Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
ENV ASPNETCORE_HTTP_PORTS=80
WORKDIR /app
COPY --from=build /app/out ./

# Start the application
ENTRYPOINT ["dotnet", "AsiaYo_Test.dll"]