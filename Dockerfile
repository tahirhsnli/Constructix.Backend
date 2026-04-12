# 1. Build & Test Mərhələsi
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Core
COPY ["src/Core/Constructix.Domain/Constructix.Domain.csproj", "src/Core/Constructix.Domain/"]
COPY ["src/Core/Constructix.Application/Constructix.Application.csproj", "src/Core/Constructix.Application/"]
# Infrastructure
COPY ["src/Infrastructure/Constructix.Infrastructure/Constructix.Infrastructure.csproj", "src/Infrastructure/Constructix.Infrastructure/"]
COPY ["src/Infrastructure/Constructix.Persistence/Constructix.Persistence.csproj", "src/Infrastructure/Constructix.Persistence/"]
# Presentation
COPY ["src/Presentation/Constructix.WebAPI/Constructix.WebAPI.csproj", "src/Presentation/Constructix.WebAPI/"]
# Tests
COPY ["test/Constructix.UnitTests/Constructix.UnitTests.csproj", "test/Constructix.UnitTests/"]
COPY ["test/Constructix.IntegrationTests/Constructix.IntegrationTests.csproj", "test/Constructix.IntegrationTests/"]

# Restore əməliyyatı
RUN dotnet restore "src/Presentation/Constructix.WebAPI/Constructix.WebAPI.csproj"

# Bütün kodları kopyalanması
COPY . .

# TESTLƏRİN QAÇIRILMASI
# Əgər hər iki test layihəsini eyni anda qaçırmaq istəyirsənsə:
RUN dotnet test "test/Constructix.UnitTests/Constructix.UnitTests.csproj" -c Release
RUN dotnet test "test/Constructix.IntegrationTests/Constructix.IntegrationTests.csproj" -c Release

# WebAPI Build
RUN dotnet build "src/Presentation/Constructix.WebAPI/Constructix.WebAPI.csproj" -c Release -o /app/build

# 2. Publish Mərhələsi
FROM build AS publish
RUN dotnet publish "src/Presentation/Constructix.WebAPI/Constructix.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. Final (Runtime)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Constructix.WebAPI.dll"]