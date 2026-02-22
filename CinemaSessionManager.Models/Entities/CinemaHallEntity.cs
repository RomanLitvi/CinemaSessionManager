using CinemaSessionManager.Models.Enums;

namespace CinemaSessionManager.Models.Entities
{
    /// <summary>
    /// Клас для зберігання даних кінозалу.
    /// Не містить обчислюваних полів та колекцій сеансів (згідно з принципом Single Responsibility).
    /// </summary>
    public class CinemaHallEntity
    {
        public int Id { get; }
        public string Name { get; set; }
        public int SeatsCount { get; set; }
        public CinemaHallType HallType { get; set; }

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
