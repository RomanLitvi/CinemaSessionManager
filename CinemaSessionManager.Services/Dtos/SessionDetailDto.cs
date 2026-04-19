using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Services.Dtos
{
    public class SessionDetailDto
    {
        public int Id { get; set; }
        public int CinemaHallId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public MovieGenre Genre { get; set; }
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);
    }
}
