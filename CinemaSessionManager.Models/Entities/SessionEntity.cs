using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Models.Entities
{
    /// <summary>
    /// Клас для зберігання даних кіносеансу.
    /// Не містить обчислюваних полів (час завершення) та посилань на об'єкт кінозалу.
    /// Зв'язок з кінозалом здійснюється через CinemaHallId.
    /// </summary>
    public class SessionEntity
    {
        public int Id { get; }
        public int CinemaHallId { get; set; }
        public string MovieTitle { get; set; }
        public MovieGenre Genre { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public SessionEntity(int id, int cinemaHallId, string movieTitle, MovieGenre genre,
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

        public override string ToString()
        {
            return $"Session #{Id}: \"{MovieTitle}\" ({Genre}, {ReleaseYear})";
        }
    }
}
