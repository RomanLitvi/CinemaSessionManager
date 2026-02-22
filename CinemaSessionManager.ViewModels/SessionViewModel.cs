using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.ViewModels
{
    /// <summary>
    /// Клас для відображення, створення та редагування кіносеансу.
    /// Містить обчислюване поле EndTime (час завершення показу).
    /// </summary>
    public class SessionViewModel
    {
        public int Id { get; set; }
        public int CinemaHallId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public MovieGenre Genre { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Час завершення показу (обчислюване поле: StartTime + DurationMinutes).
        /// </summary>
        public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);

        public SessionViewModel() { }

        public SessionViewModel(int id, int cinemaHallId, string movieTitle, MovieGenre genre,
            int releaseYear, DateTime startTime, int durationMinutes)
        {
            Id = id;
            CinemaHallId = cinemaHallId;
            MovieTitle = movieTitle;
            Genre = genre;
            ReleaseYear = releaseYear;
            StartTime = startTime;
            DurationMinutes = durationMinutes;
        }

        /// <summary>
        /// Коротка інформація про сеанс (для списку).
        /// </summary>
        public string ToShortString()
        {
            return $"  [{Id}] \"{MovieTitle}\" | {StartTime:HH:mm}-{EndTime:HH:mm} | {Genre}";
        }

        /// <summary>
        /// Повна інформація про сеанс (для детального перегляду).
        /// </summary>
        public string ToDetailedString()
        {
            return $"Кіносеанс #{Id}\n" +
                   $"  Фільм: \"{MovieTitle}\"\n" +
                   $"  Жанр: {Genre}\n" +
                   $"  Рік випуску: {ReleaseYear}\n" +
                   $"  Початок: {StartTime:HH:mm}\n" +
                   $"  Тривалість: {DurationMinutes} хв\n" +
                   $"  Завершення: {EndTime:HH:mm}";
        }

        public override string ToString()
        {
            return ToShortString();
        }
    }
}
