# Менеджер кіносеансів (Cinema Session Manager)

Десктопний застосунок для обліку кінозалів і кіносеансів на C# (.NET 8, .NET MAUI).

## Структура рішення

| Проєкт | Тип | Призначення |
|--------|-----|-------------|
| `CinemaSessionManager.Models` | Бібліотека класів | Entity-класи та перечислення (Enum) |
| `CinemaSessionManager.Repositories` | Бібліотека класів | Інтерфейси репозиторіїв, JSON-сховище даних |
| `CinemaSessionManager.Services` | Бібліотека класів | Сервіси, DTO, реєстрація DI |
| `CinemaSessionManager.MauiApp` | .NET MAUI App | UI, ViewModels (MVVM), навігація |

## Основні сутності

### Кінозал (CinemaHall) — сутність 1-го рівня
- Id, Name, SeatsCount, HallType (TwoD, ThreeD, IMAX, FourDX, ScreenX)

### Кіносеанс (Session) — сутність 2-го рівня
- Id, CinemaHallId, MovieTitle, Genre, ReleaseYear, StartTime, DurationMinutes

## Архітектура та принципи

- **3+ шари**: Models → Repositories → Services → MauiApp (UI)
- **MVVM**: ViewModels з `INotifyPropertyChanged`, `RelayCommand`, `AsyncRelayCommand`
- **Dependency Injection** через `Microsoft.Extensions.DependencyInjection`
- **Inversion of Control** — взаємодія зі сховищем через інтерфейси `IDataStore`, `ICinemaHallRepository`, `ISessionRepository`
- **Single Responsibility** — Entity зберігає дані, DTO передає, ViewModel відповідає за UI-стан
- **Async/await** — усі операції зі сховищем асинхронні, UI не блокується
- **ActivityIndicator** + блокування UI під час async-операцій

## Сховище даних

JSON-файл (`cinema_data.json`) у директорії застосунку. При першому запуску створюється з seed-даними. Підтримує каскадне видалення сеансів при видаленні кінозалу.

## Функціонал

- CRUD для кінозалів та сеансів
- Пошук / фільтрація + сортування (за назвою, типом, кількістю місць тощо)
- Навігація через Shell (одне вікно, заміна сторінок)
- Редагування полів у detail-view з режимами перегляду/редагування

## Як запустити

### macOS

```bash
dotnet workload install maui
dotnet run --project CinemaSessionManager.MauiApp -f net8.0-maccatalyst
```

### Windows

Відкрити `CinemaSessionManager.sln` у Visual Studio та запустити проєкт `CinemaSessionManager.MauiApp`.
