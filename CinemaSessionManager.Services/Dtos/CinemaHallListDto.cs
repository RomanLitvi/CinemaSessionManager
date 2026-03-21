namespace CinemaSessionManager.Services.Dtos
{
    /// <summary>
    /// DTO для відображення кінозалу у списку.
    /// Містить лише поля, необхідні для елемента списку.
    /// </summary>
    public class CinemaHallListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string HallType { get; set; } = string.Empty;
        public int SeatsCount { get; set; }
        public int SessionCount { get; set; }
    }
}
