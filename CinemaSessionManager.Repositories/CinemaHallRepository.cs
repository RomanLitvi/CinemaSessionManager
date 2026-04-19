using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Repositories.Storage;

namespace CinemaSessionManager.Repositories
{
    public class CinemaHallRepository : ICinemaHallRepository
    {
        private readonly IDataStore _dataStore;

        public CinemaHallRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<List<CinemaHallEntity>> GetAllAsync()
            => _dataStore.GetAllCinemaHallsAsync();

        public Task<CinemaHallEntity?> GetByIdAsync(int id)
            => _dataStore.GetCinemaHallByIdAsync(id);

        public Task AddAsync(CinemaHallEntity hall)
            => _dataStore.AddCinemaHallAsync(hall);

        public Task UpdateAsync(CinemaHallEntity hall)
            => _dataStore.UpdateCinemaHallAsync(hall);

        public Task DeleteAsync(int id)
            => _dataStore.DeleteCinemaHallAsync(id);

        public Task<int> GenerateNextIdAsync()
            => _dataStore.GenerateNextHallIdAsync();
    }
}
