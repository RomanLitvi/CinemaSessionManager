# ===== Stage 1: Restore =====
# Копіюємо лише .csproj файли для кешування залежностей.
# Docker кешує цей шар — при зміні коду залежності не перевстановлюються.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src

COPY CinemaSessionManager.Models/CinemaSessionManager.Models.csproj CinemaSessionManager.Models/
COPY CinemaSessionManager.ViewModels/CinemaSessionManager.ViewModels.csproj CinemaSessionManager.ViewModels/
COPY CinemaSessionManager.Services/CinemaSessionManager.Services.csproj CinemaSessionManager.Services/
COPY CinemaSessionManager.ConsoleApp/CinemaSessionManager.ConsoleApp.csproj CinemaSessionManager.ConsoleApp/

RUN dotnet restore CinemaSessionManager.ConsoleApp/CinemaSessionManager.ConsoleApp.csproj

# ===== Stage 2: Build & Publish =====
# Копіюємо весь код та створюємо оптимізований реліз-білд.
FROM restore AS build
COPY . .

RUN dotnet publish CinemaSessionManager.ConsoleApp/CinemaSessionManager.ConsoleApp.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# ===== Stage 3: Runtime =====
# Фінальний образ — лише runtime без SDK (~80 МБ замість ~700 МБ).
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine AS runtime

LABEL maintainer="litvinchukroman"
LABEL description="CinemaSessionManager Console Application"

RUN adduser -D -h /app appuser
USER appuser
WORKDIR /app

COPY --from=build --chown=appuser:appuser /app/publish .

ENTRYPOINT ["dotnet", "CinemaSessionManager.ConsoleApp.dll"]
