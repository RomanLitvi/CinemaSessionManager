using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Models.Entities
{
    public class SessionEntity
    {
        public int Id { get; set; }
        public int CinemaHallId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public MovieGenre Genre { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public SessionEntity() { }

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
