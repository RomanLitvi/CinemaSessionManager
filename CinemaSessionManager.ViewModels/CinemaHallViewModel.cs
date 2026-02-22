using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.ViewModels
{
    /// <summary>
    /// Клас для відображення, створення та редагування кінозалу.
    /// Містить обчислюване поле TotalSessionsDurationMinutes та колекцію сеансів.
    /// </summary>
    public class CinemaHallViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SeatsCount { get; set; }
        public CinemaHallType HallType { get; set; }

        public List<SessionViewModel> Sessions { get; set; } = new List<SessionViewModel>();

        /// <summary>
        /// Загальна тривалість усіх кіносеансів у хвилинах (обчислюване поле).
        /// </summary>
        public int TotalSessionsDurationMinutes
        {
            get
            {
                int total = 0;
                foreach (var session in Sessions)
                {
                    total += session.DurationMinutes;
                }
                return total;
            }
        }

        public CinemaHallViewModel() { }

        public CinemaHallViewModel(int id, string name, int seatsCount, CinemaHallType hallType)
        {
            Id = id;
            Name = name;
            SeatsCount = seatsCount;
            HallType = hallType;
        }

        /// <summary>
        /// Коротка інформація про кінозал (для списку).
        /// </summary>
        public string ToShortString()
        {
            return $"[{Id}] {Name} | {HallType} | {SeatsCount} місць | Сеансів: {Sessions.Count}";
        }

        /// <summary>
        /// Повна інформація про кінозал (для детального перегляду).
        /// </summary>
        public string ToDetailedString()
        {
            return $"Кінозал #{Id}\n" +
                   $"  Назва: {Name}\n" +
                   $"  Тип: {HallType}\n" +
                   $"  Кількість місць: {SeatsCount}\n" +
                   $"  Кількість сеансів: {Sessions.Count}\n" +
                   $"  Загальна тривалість сеансів: {TotalSessionsDurationMinutes} хв";
        }

        public override string ToString()
        {
            return ToShortString();
        }
    }
}
