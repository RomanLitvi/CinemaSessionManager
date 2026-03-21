namespace CinemaSessionManager.Services.Dtos
{
    /// <summary>
    /// DTO для відображення детальної інформації про кіносеанс.
    /// Містить усі видимі поля, включаючи назву кінозалу.
    /// </summary>
    public class SessionDetailDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public string HallName { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int ReleaseYear { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Час завершення показу (обчислюване поле).
        /// </summary>
        public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);
    }
}
