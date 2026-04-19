using CinemaSessionManager.Models.Entities;

namespace CinemaSessionManager.Repositories.Storage
{
    public interface IDataStore
    {
        Task<List<CinemaHallEntity>> GetAllCinemaHallsAsync();
        Task<CinemaHallEntity?> GetCinemaHallByIdAsync(int id);
        Task AddCinemaHallAsync(CinemaHallEntity hall);
        Task UpdateCinemaHallAsync(CinemaHallEntity hall);
        Task DeleteCinemaHallAsync(int id);

        Task<List<SessionEntity>> GetSessionsByHallIdAsync(int hallId);
        Task<SessionEntity?> GetSessionByIdAsync(int id);
        Task AddSessionAsync(SessionEntity session);
        Task UpdateSessionAsync(SessionEntity session);
        Task DeleteSessionAsync(int id);
        Task DeleteSessionsByHallIdAsync(int hallId);

        Task<int> GenerateNextHallIdAsync();
        Task<int> GenerateNextSessionIdAsync();
    }
}
