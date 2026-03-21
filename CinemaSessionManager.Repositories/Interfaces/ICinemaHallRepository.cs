using CinemaSessionManager.Models.Entities;

namespace CinemaSessionManager.Repositories.Interfaces
{
    /// <summary>
    /// Інтерфейс репозиторію для роботи з кінозалами.
    /// </summary>
    public interface ICinemaHallRepository
    {
        List<CinemaHallEntity> GetAll();
        CinemaHallEntity? GetById(int id);
    }
}
