using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Models.Entities
{
    public class CinemaHallEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SeatsCount { get; set; }
        public CinemaHallType HallType { get; set; }

        public CinemaHallEntity() { }

        public CinemaHallEntity(int id, string name, int seatsCount, CinemaHallType hallType)
        {
            Id = id;
            Name = name;
            SeatsCount = seatsCount;
            HallType = hallType;
        }

        public override string ToString()
        {
            return $"CinemaHall #{Id}: {Name} ({HallType}, {SeatsCount} місць)";
        }
    }
}
