# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
# For more information, please see https://aka.ms/containercompat

# This stage is used when running from VS in fast mode (Default for Debug configuration)
# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0.203 AS build
WORKDIR /src
COPY ["SwimmingAcademy.csproj", "."]
RUN dotnet restore "SwimmingAcademy.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "SwimmingAcademy.csproj" -c Release -o /app/build --no-restore

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "SwimmingAcademy.csproj" -c Release -o /app/publish --no-build

# Stage 3: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0.5 AS final
WORKDIR /app
EXPOSE 80

# Copy the published application from the publish stage
COPY --from=publish /app/publish .

# Define a non-root user (already available in aspnet:8.0 images)
# For production, it's generally good practice to run as a non-root user
USER app

ENTRYPOINT ["dotnet", "SwimmingAcademy.dll"]