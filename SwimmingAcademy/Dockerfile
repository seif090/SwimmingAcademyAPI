# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0.203 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["SwimmingAcademy.csproj", "./"]
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet build "SwimmingAcademy.csproj" -c Release -o /app/build --no-restore

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "SwimmingAcademy.csproj" -c Release -o /app/publish --no-build

# Stage 3: Create final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.5 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use a non-root user (already built into aspnet image)
USER app

# Copy published output
COPY --from=publish /app/publish .

# Start the application
ENTRYPOINT ["dotnet", "SwimmingAcademy.dll"]
