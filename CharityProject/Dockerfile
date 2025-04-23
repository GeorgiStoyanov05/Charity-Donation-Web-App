# Build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy everything and restore
COPY . .
WORKDIR /src/CharityProject
RUN dotnet restore
RUN dotnet publish -c Release -o /app

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# 👇 Add this to expose the port Render expects
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

COPY --from=build /app .

# 👇 Start the app
ENTRYPOINT ["dotnet", "CharityProject.dll"]
