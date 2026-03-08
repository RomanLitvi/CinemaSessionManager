using CinemaSessionManager.ViewModels;

namespace CinemaSessionManager.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс сервісу для роботи з кінозалами та сеансами.
    /// </summary>
    public interface ICinemaService
    {
        List<CinemaHallViewModel> GetAllCinemaHalls();
        CinemaHallViewModel? GetCinemaHallWithSessions(int hallId);
        void LoadSessions(CinemaHallViewModel hallViewModel);
    }
}
