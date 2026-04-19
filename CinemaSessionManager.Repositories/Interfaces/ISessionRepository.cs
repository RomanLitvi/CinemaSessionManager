using CinemaSessionManager.Models.Entities;

namespace CinemaSessionManager.Repositories.Interfaces
{
    public interface ISessionRepository
    {
        Task<List<SessionEntity>> GetByHallIdAsync(int hallId);
        Task<SessionEntity?> GetByIdAsync(int id);
        Task AddAsync(SessionEntity session);
        Task UpdateAsync(SessionEntity session);
        Task DeleteAsync(int id);
        Task DeleteByHallIdAsync(int hallId);
        Task<int> GenerateNextIdAsync();
    }
}
