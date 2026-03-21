namespace CinemaSessionManager.Services.Dtos
{
    /// <summary>
    /// DTO для відображення кіносеансу у списку залу.
    /// Містить поля для елемента списку сеансів.
    /// </summary>
    public class SessionListDto
    {
        public int Id { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
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
