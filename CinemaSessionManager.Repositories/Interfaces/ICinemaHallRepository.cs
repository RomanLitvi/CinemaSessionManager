using CinemaSessionManager.Models.Entities;

namespace CinemaSessionManager.Repositories.Interfaces
{
    public interface ICinemaHallRepository
    {
        Task<List<CinemaHallEntity>> GetAllAsync();
        Task<CinemaHallEntity?> GetByIdAsync(int id);
        Task AddAsync(CinemaHallEntity hall);
        Task UpdateAsync(CinemaHallEntity hall);
        Task DeleteAsync(int id);
        Task<int> GenerateNextIdAsync();
    }
}
