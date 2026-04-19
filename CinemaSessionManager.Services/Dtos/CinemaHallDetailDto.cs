using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Services.Dtos
{
    public class CinemaHallDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CinemaHallType HallType { get; set; }
        public int SeatsCount { get; set; }
        public List<SessionListDto> Sessions { get; set; } = new();

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
