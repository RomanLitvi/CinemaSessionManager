using CinemaSessionManager.Models.Entities;

namespace CinemaSessionManager.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс сховища даних кінозалів та сеансів.
    /// </summary>
    public interface IDataStore
    {
        List<CinemaHallEntity> GetCinemaHalls();
        List<SessionEntity> GetSessionsByHallId(int hallId);
        List<SessionEntity> GetAllSessions();
    }
}
