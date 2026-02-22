using CinemaSessionManager.Services;
using CinemaSessionManager.ViewModels;

namespace CinemaSessionManager.ConsoleApp
{
    /// <summary>
    /// Головний клас консольного застосунку "Менеджер кіносеансів".
    /// Дозволяє переглядати кінозали, обирати конкретний зал,
    /// переглядати список сеансів та детальну інформацію по кожному.
    /// </summary>
    internal class Program
    {
        private static readonly CinemaService _cinemaService = new CinemaService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Менеджер кіносеансів ===\n");

            bool running = true;

            while (running)
            {
                // Отримуємо список кінозалів через сервіс
                List<CinemaHallViewModel> halls = _cinemaService.GetAllCinemaHalls();

                // Завантажуємо сеанси для кожного залу (щоб показати кількість)
                foreach (var hall in halls)
                {
                    _cinemaService.LoadSessions(hall);
                }

                Console.WriteLine("--- Список кінозалів ---\n");
                foreach (var hall in halls)
                {
                    Console.WriteLine(hall.ToShortString());
                }
                Console.WriteLine();

                Console.WriteLine("Введіть ID кінозалу для перегляду деталей або 0 для виходу:");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int selectedId))
                {
                    Console.WriteLine("Невірне введення. Спробуйте ще раз.\n");
                    continue;
                }

                if (selectedId == 0)
                {
                    running = false;
                    continue;
                }

                // Отримуємо кінозал з сеансами
                CinemaHallViewModel? selectedHall = _cinemaService.GetCinemaHallWithSessions(selectedId);

                if (selectedHall == null)
                {
                    Console.WriteLine($"Кінозал з ID {selectedId} не знайдено.\n");
                    continue;
                }

                ShowHallDetails(selectedHall);
            }

            Console.WriteLine("\nДякуємо за використання! До побачення.");
        }

        /// <summary>
        /// Показує детальну інформацію по кінозалу та його сеанси.
        /// Дає можливість переглянути деталі конкретного сеансу.
        /// </summary>
        private static void ShowHallDetails(CinemaHallViewModel hall)
        {
            bool viewingHall = true;

            while (viewingHall)
            {
                Console.WriteLine();
                Console.WriteLine("========================================");
                Console.WriteLine(hall.ToDetailedString());
                Console.WriteLine("========================================");

                if (hall.Sessions.Count == 0)
                {
                    Console.WriteLine("\n  У цьому залі немає запланованих сеансів.");
                }
                else
                {
                    Console.WriteLine("\n--- Кіносеанси ---\n");
                    foreach (var session in hall.Sessions)
                    {
                        Console.WriteLine(session.ToShortString());
                    }
                }

                Console.WriteLine();
                Console.WriteLine("Оберіть дію:");
                Console.WriteLine("  Введіть ID сеансу - переглянути повну інформацію");
                Console.WriteLine("  0 - повернутись до списку кінозалів");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int sessionId))
                {
                    Console.WriteLine("Невірне введення. Спробуйте ще раз.");
                    continue;
                }

                if (sessionId == 0)
                {
                    viewingHall = false;
                    continue;
                }

                // Шукаємо сеанс серед сеансів обраного залу
                SessionViewModel? selectedSession = null;
                foreach (var session in hall.Sessions)
                {
                    if (session.Id == sessionId)
                    {
                        selectedSession = session;
                        break;
                    }
                }

                if (selectedSession == null)
                {
                    Console.WriteLine($"Сеанс з ID {sessionId} не знайдено в цьому залі.");
                    continue;
                }

                Console.WriteLine();
                Console.WriteLine("----------------------------------------");
                Console.WriteLine(selectedSession.ToDetailedString());
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("\nНатисніть Enter, щоб повернутись...");
                Console.ReadLine();
            }
        }
    }
}
