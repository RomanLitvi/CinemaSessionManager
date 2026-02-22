using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.ViewModels;

namespace CinemaSessionManager.Services
{
    /// <summary>
    /// Сервіс для роботи зі сховищем даних.
    /// Відповідає за отримання даних зі сховища та перетворення Entity -> ViewModel.
    /// </summary>
    public class CinemaService
    {
        /// <summary>
        /// Отримує список усіх кінозалів у вигляді ViewModel (без завантажених сеансів).
        /// </summary>
        public List<CinemaHallViewModel> GetAllCinemaHalls()
        {
            List<CinemaHallEntity> entities = FakeDataStore.GetCinemaHalls();
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
            List<CinemaHallEntity> halls = FakeDataStore.GetCinemaHalls();
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
            List<SessionEntity> sessionEntities = FakeDataStore.GetSessionsByHallId(hallViewModel.Id);
            hallViewModel.Sessions.Clear();

            foreach (var entity in sessionEntities)
            {
                hallViewModel.Sessions.Add(MapToViewModel(entity));
            }
        }

        /// <summary>
        /// Перетворення CinemaHallEntity -> CinemaHallViewModel.
        /// </summary>
        private CinemaHallViewModel MapToViewModel(CinemaHallEntity entity)
        {
            return new CinemaHallViewModel(entity.Id, entity.Name, entity.SeatsCount, entity.HallType);
        }

        /// <summary>
        /// Перетворення SessionEntity -> SessionViewModel.
        /// </summary>
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
