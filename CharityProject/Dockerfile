# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy solution and project files
COPY . ./
WORKDIR /src/CharityProject

RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app .

# Run the app
ENTRYPOINT ["dotnet", "CharityProject.dll"]
