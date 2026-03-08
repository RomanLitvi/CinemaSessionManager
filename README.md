# Менеджер кіносеансів (Cinema Session Manager)

Застосунок для обліку кінозалів і кіносеансів на C# (.NET 8).  
Включає консольний застосунок та графічний UI на .NET MAUI.

> **Docker, Docker Compose та CI/CD** додані як експеримент — для вивчення контейнеризації та автоматизації збірки. В подальшому планується перетворити C#-проєкт на повноцінний бекенд. Ця частина не є вимогою лабораторної роботи.

## Структура рішення

| Проєкт | Тип | Призначення |
|--------|-----|-------------|
| `CinemaSessionManager.Models` | Бібліотека класів | Entity-класи та перечислення (Enum) |
| `CinemaSessionManager.ViewModels` | Бібліотека класів | ViewModel-класи для відображення з обчислюваними полями |
| `CinemaSessionManager.Services` | Бібліотека класів | Інтерфейси, сервіси, сховище даних, реєстрація DI |
| `CinemaSessionManager.ConsoleApp` | Консольний застосунок | Консольний інтерфейс користувача |
| `CinemaSessionManager.MauiApp` | .NET MAUI App | Графічний UI з навігацією між сторінками |

## Основні сутності

### Кінозал (CinemaHall) — сутність 1-го рівня
- **CinemaHallEntity** — Id, Name, SeatsCount, HallType
- **CinemaHallViewModel** — додатково: колекція Sessions, обчислюване TotalSessionsDurationMinutes

### Кіносеанс (Session) — сутність 2-го рівня
- **SessionEntity** — Id, CinemaHallId, MovieTitle, Genre, ReleaseYear, StartTime, DurationMinutes
- **SessionViewModel** — додатково: обчислюване EndTime

### Перечислення
- **CinemaHallType** — TwoD, ThreeD, IMAX, FourDX, ScreenX
- **MovieGenre** — Comedy, Horror, Animation, Drama, Action, SciFi, Romance, Thriller

## Архітектура та принципи

- **Dependency Injection (DI)** — сервіси реєструються через IoC-контейнер (`Microsoft.Extensions.DependencyInjection`)
- **Inversion of Control** — взаємодія зі сховищем через інтерфейси `IDataStore` та `ICinemaService`
- **Single Responsibility** — Entity зберігає дані, ViewModel відповідає за відображення
- **Інкапсуляція** — `FakeDataStore` є `internal`, реєструється через `ServiceCollectionExtensions`

## UI-застосунок (.NET MAUI)

Три сторінки з навігацією:

1. **CinemaHallsListPage** — список усіх кінозалів
2. **CinemaHallDetailsPage** — деталі кінозалу + список сеансів
3. **SessionDetailsPage** — деталі обраного сеансу

Навігація реалізована через `NavigationPage` (стек сторінок) з можливістю повернення назад.

## Як запустити

### Консольний застосунок

```bash
cd CinemaSessionManager
dotnet run --project CinemaSessionManager.ConsoleApp
```

### MAUI-застосунок (macOS)

Потрібно: .NET 8 SDK (повний, не Homebrew), Xcode, MAUI workload.

```bash
dotnet workload install maui
cd CinemaSessionManager
dotnet run --project CinemaSessionManager.MauiApp -f net8.0-maccatalyst
```

### MAUI-застосунок (Windows)

Відкрити `CinemaSessionManager.sln` у Visual Studio та запустити проєкт `CinemaSessionManager.MauiApp`.

## Docker

### Збірка та запуск локально

```bash
cd CinemaSessionManager
docker build -t cinema-manager .
docker run -it cinema-manager
```

### Запуск через Docker Compose (образ з Docker Hub)

```bash
cd CinemaSessionManager
docker compose run cinema-console-app
```

### Multi-stage Dockerfile

| Stage | Базовий образ | Призначення |
|-------|--------------|-------------|
| `restore` | `sdk:8.0` | Відновлення NuGet-залежностей (кешується) |
| `build` | `sdk:8.0` | Збірка та публікація Release-версії |
| `runtime` | `runtime:8.0-alpine` | Фінальний легкий образ (~80 МБ) |

## CI/CD (GitHub Actions)

Автоматичний пайплайн при push в `main`/`master`:

1. **Build** — відновлення залежностей та збірка .NET проєкту
2. **Docker** — збірка Docker-образу та push в Docker Hub

