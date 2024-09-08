# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ACIDemo.csproj", "."]
RUN dotnet restore "./ACIDemo.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ACIDemo.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ACIDemo.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACIDemo.dll"]

# docker build -t acidemowebapi:V1 .
# docker run -d -p 5200:8080 -p 5201:8081 --env=ASPNETCORE_HTTP_PORTS=8080 --env=DOTNET_RUNNING_IN_CONTAINER=true --env=DOTNET_VERSION=8.0.8 --env=ASPNET_VERSION=8.0.8 --name acidemowebapicontainer acidemowebapi:V1