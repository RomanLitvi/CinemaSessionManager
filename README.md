# Менеджер кіносеансів (Cinema Session Manager)

Консольний застосунок для обліку кінозалів і кіносеансів на C# (.NET 8).

## Структура рішення

Рішення складається з 4 проєктів:

| Проєкт | Тип | Призначення |
|--------|-----|-------------|
| `CinemaSessionManager.Models` | Бібліотека класів | Класи для зберігання даних (Entity) та перечислення (Enum) |
| `CinemaSessionManager.ViewModels` | Бібліотека класів | Класи для відображення, створення та редагування сутностей |
| `CinemaSessionManager.Services` | Бібліотека класів | Сервіс взаємодії зі сховищем, штучне сховище даних |
| `CinemaSessionManager.ConsoleApp` | Консольний застосунок | Точка входу, інтерфейс користувача |

## Основні сутності

### Кінозал (CinemaHall)
- **CinemaHallEntity** — клас для зберігання: Id, Name, SeatsCount, HallType
- **CinemaHallViewModel** — клас для відображення: додатково містить колекцію сеансів і обчислюване поле TotalSessionsDurationMinutes

### Кіносеанс (Session)
- **SessionEntity** — клас для зберігання: Id, CinemaHallId, MovieTitle, Genre, ReleaseYear, StartTime, DurationMinutes
- **SessionViewModel** — клас для відображення: додатково містить обчислюване поле EndTime

### Перечислення
- **CinemaHallType** — тип залу: TwoD, ThreeD, IMAX, FourDX, ScreenX
- **MovieGenre** — жанр фільму: Comedy, Horror, Animation, Drama, Action, SciFi, Romance, Thriller

## Принципи проєктування

- **Single Responsibility**: Entity-класи відповідають лише за зберігання даних, ViewModel-класи — за відображення та взаємодію
- **Композиція**: CinemaHallViewModel містить колекцію SessionViewModel
- **Багатошарова архітектура**: Models -> ViewModels -> Services -> ConsoleApp
- **Інкапсуляція сховища**: FakeDataStore є internal-класом, доступним лише через CinemaService

## Як запустити

1. Встановити [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Виконати в терміналі:

```bash
cd CinemaSessionManager
dotnet build
dotnet run --project CinemaSessionManager.ConsoleApp
```

## Логіка роботи консольного застосунку

1. При запуску відображається список усіх кінозалів
2. Користувач вводить ID кінозалу для перегляду деталей
3. Відображається інформація про кінозал та список його кіносеансів
4. Користувач може ввести ID сеансу для перегляду повної інформації
5. Введення 0 — повернення до списку кінозалів або вихід з програми
