using CinemaSessionManager.Services.Dtos;

namespace CinemaSessionManager.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс сервісу для роботи з кінозалами.
    /// </summary>
    public interface ICinemaHallService
    {
        List<CinemaHallListDto> GetAllHalls();
        CinemaHallDetailDto? GetHallDetails(int hallId);
    }
}
