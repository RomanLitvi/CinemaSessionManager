using CinemaSessionManager.Models.Entities;

namespace CinemaSessionManager.Repositories.Interfaces
{
    /// <summary>
    /// Інтерфейс репозиторію для роботи з кіносеансами.
    /// </summary>
    public interface ISessionRepository
    {
        List<SessionEntity> GetByHallId(int hallId);
        SessionEntity? GetById(int id);
    }
}
