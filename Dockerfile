# Pull dotnet image for build
FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /app

# Set args for build
ARG BUILD_CONFIG=RELEASE
ARG VERSION=1.0.0

# Copy sln
COPY *.sln .

# Copy .csjproj files
COPY ./src/Ceii.Api.Core/*.csproj ./src/Ceii.Api.Core/
COPY ./src/Ceii.Api.Data/*.csproj ./src/Ceii.Api.Data/
COPY ./src/Ceii.Api.Services/*.csproj ./src/Ceii.Api.Services/

# Restore NuGET Packages
RUN dotnet restore

# Copy everything else
COPY . .

RUN dotnet publish -c ${BUILD_CONFIG} -o out

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal as runtime
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 5000

ENTRYPOINT [ "dotnet", "Ceii.Api.Core.dll" ]