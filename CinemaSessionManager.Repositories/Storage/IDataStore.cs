using CinemaSessionManager.Models.Entities;

namespace CinemaSessionManager.Repositories.Storage
{
    /// <summary>
    /// Інтерфейс внутрішнього сховища даних.
    /// Доступний лише в межах проєкту Repositories.
    /// </summary>
    public interface IDataStore
    {
        List<CinemaHallEntity> GetCinemaHalls();
        List<SessionEntity> GetSessionsByHallId(int hallId);
        SessionEntity? GetSessionById(int sessionId);
    }
}
