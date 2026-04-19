using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Repositories.Storage;

namespace CinemaSessionManager.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IDataStore _dataStore;

        public SessionRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public Task<List<SessionEntity>> GetByHallIdAsync(int hallId)
            => _dataStore.GetSessionsByHallIdAsync(hallId);

        public Task<SessionEntity?> GetByIdAsync(int id)
            => _dataStore.GetSessionByIdAsync(id);

        public Task AddAsync(SessionEntity session)
            => _dataStore.AddSessionAsync(session);

        public Task UpdateAsync(SessionEntity session)
            => _dataStore.UpdateSessionAsync(session);

        public Task DeleteAsync(int id)
            => _dataStore.DeleteSessionAsync(id);

        public Task DeleteByHallIdAsync(int hallId)
            => _dataStore.DeleteSessionsByHallIdAsync(hallId);

        public Task<int> GenerateNextIdAsync()
            => _dataStore.GenerateNextSessionIdAsync();
    }
}
