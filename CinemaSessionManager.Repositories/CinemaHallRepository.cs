using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Repositories.Storage;

namespace CinemaSessionManager.Repositories
{
    /// <summary>
    /// Репозиторій для отримання даних про кінозали зі сховища.
    /// </summary>
    public class CinemaHallRepository : ICinemaHallRepository
    {
        private readonly IDataStore _dataStore;

        public CinemaHallRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public List<CinemaHallEntity> GetAll()
        {
            return _dataStore.GetCinemaHalls();
        }

        public CinemaHallEntity? GetById(int id)
        {
            foreach (var hall in _dataStore.GetCinemaHalls())
            {
                if (hall.Id == id)
                    return hall;
            }
            return null;
        }
    }
}
