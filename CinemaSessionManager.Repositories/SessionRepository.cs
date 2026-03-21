using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Repositories.Storage;

namespace CinemaSessionManager.Repositories
{
    /// <summary>
    /// Репозиторій для отримання даних про кіносеанси зі сховища.
    /// </summary>
    public class SessionRepository : ISessionRepository
    {
        private readonly IDataStore _dataStore;

        public SessionRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public List<SessionEntity> GetByHallId(int hallId)
        {
            return _dataStore.GetSessionsByHallId(hallId);
        }

        public SessionEntity? GetById(int id)
        {
            return _dataStore.GetSessionById(id);
        }
    }
}
