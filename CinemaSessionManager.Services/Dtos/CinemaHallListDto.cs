using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Services.Dtos
{
    public class CinemaHallListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public CinemaHallType HallType { get; set; }
        public int SeatsCount { get; set; }
        public int SessionCount { get; set; }
    }
}
