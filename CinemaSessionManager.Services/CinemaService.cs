using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Services.Interfaces;
using CinemaSessionManager.ViewModels;

namespace CinemaSessionManager.Services
{
    /// <summary>
    /// Сервіс для роботи зі сховищем даних.
    /// Відповідає за отримання даних зі сховища та перетворення Entity -> ViewModel.
    /// </summary>
    public class CinemaService : ICinemaService
    {
        private readonly IDataStore _dataStore;

        public CinemaService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        /// <summary>
        /// Отримує список усіх кінозалів у вигляді ViewModel (без завантажених сеансів).
        /// </summary>
        public List<CinemaHallViewModel> GetAllCinemaHalls()
        {
            List<CinemaHallEntity> entities = _dataStore.GetCinemaHalls();
            var viewModels = new List<CinemaHallViewModel>();

            foreach (var entity in entities)
            {
                viewModels.Add(MapToViewModel(entity));
            }

            return viewModels;
        }

        /// <summary>
        /// Отримує кінозал за ідентифікатором разом із сеансами.
        /// </summary>
        public CinemaHallViewModel? GetCinemaHallWithSessions(int hallId)
        {
            List<CinemaHallEntity> halls = _dataStore.GetCinemaHalls();
            CinemaHallEntity? hallEntity = null;

            foreach (var hall in halls)
            {
                if (hall.Id == hallId)
                {
                    hallEntity = hall;
                    break;
                }
            }

            if (hallEntity == null)
                return null;

            var viewModel = MapToViewModel(hallEntity);
            LoadSessions(viewModel);

            return viewModel;
        }

        /// <summary>
        /// Завантажує сеанси для конкретного кінозалу.
        /// </summary>
        public void LoadSessions(CinemaHallViewModel hallViewModel)
        {
            List<SessionEntity> sessionEntities = _dataStore.GetSessionsByHallId(hallViewModel.Id);
            hallViewModel.Sessions.Clear();

            foreach (var entity in sessionEntities)
            {
                hallViewModel.Sessions.Add(MapToViewModel(entity));
            }
        }

        private CinemaHallViewModel MapToViewModel(CinemaHallEntity entity)
        {
            return new CinemaHallViewModel(entity.Id, entity.Name, entity.SeatsCount, entity.HallType);
        }

        private SessionViewModel MapToViewModel(SessionEntity entity)
        {
            return new SessionViewModel(
                entity.Id,
                entity.CinemaHallId,
                entity.MovieTitle,
                entity.Genre,
                entity.ReleaseYear,
                entity.StartTime,
                entity.DurationMinutes
            );
        }
    }
}
