# Create build image
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-environment
WORKDIR /app

# Copy csproj files from build context (this directory) and move them to the workdir (/app) on the container
COPY *.csproj ./
# Run restore to install the needed dependencies defined in the csproj file
RUN dotnet restore
# Copy all other files from the build context (this dir) to the workdir
COPY . ./
RUN dotnet publish -o out

# build RUNTIME image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1