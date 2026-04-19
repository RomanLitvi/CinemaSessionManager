using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Services.Dtos
{
    public class SessionListDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public MovieGenre Genre { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);
    }
}
