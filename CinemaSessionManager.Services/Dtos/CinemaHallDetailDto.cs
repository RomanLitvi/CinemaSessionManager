namespace CinemaSessionManager.Services.Dtos
{
    /// <summary>
    /// DTO для відображення детальної інформації про кінозал.
    /// Містить усі видимі поля, включаючи список сеансів.
    /// </summary>
    public class CinemaHallDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HallType { get; set; } = string.Empty;
        public int SeatsCount { get; set; }
        public List<SessionListDto> Sessions { get; set; } = new List<SessionListDto>();

        /// <summary>
        /// Загальна тривалість усіх сеансів у хвилинах (обчислюване поле).
        /// </summary>
        public int TotalSessionsDurationMinutes
        {
            get
            {
                int total = 0;
                foreach (var session in Sessions)
                    total += session.DurationMinutes;
                return total;
            }
        }

        public int SessionCount => Sessions.Count;
    }
}
