using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.Services
{
    /// <summary>
    /// Сервіс для роботи з кінозалами.
    /// Отримує дані з репозиторіїв, конвертує Entity → DTO і повертає для використання у UI.
    /// </summary>
    public class CinemaHallService : ICinemaHallService
    {
        private readonly ICinemaHallRepository _hallRepository;
        private readonly ISessionRepository _sessionRepository;

        public CinemaHallService(ICinemaHallRepository hallRepository, ISessionRepository sessionRepository)
        {
            _hallRepository = hallRepository;
            _sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Повертає список усіх кінозалів з кількістю сеансів для відображення у списку.
        /// </summary>
        public List<CinemaHallListDto> GetAllHalls()
        {
            var halls = _hallRepository.GetAll();
            var result = new List<CinemaHallListDto>();

            foreach (var hall in halls)
            {
                var sessions = _sessionRepository.GetByHallId(hall.Id);
                result.Add(new CinemaHallListDto
                {
                    Id = hall.Id,
                    Name = hall.Name,
                    HallType = hall.HallType.ToString(),
                    SeatsCount = hall.SeatsCount,
                    SessionCount = sessions.Count
                });
            }

            return result;
        }

        /// <summary>
        /// Повертає детальну інформацію про кінозал разом зі списком сеансів.
        /// </summary>
        public CinemaHallDetailDto? GetHallDetails(int hallId)
        {
            var hall = _hallRepository.GetById(hallId);
            if (hall == null)
                return null;

            var sessions = _sessionRepository.GetByHallId(hallId);
            var sessionDtos = new List<SessionListDto>();

            foreach (var session in sessions)
            {
                sessionDtos.Add(new SessionListDto
                {
                    Id = session.Id,
                    MovieTitle = session.MovieTitle,
                    Genre = session.Genre.ToString(),
                    ReleaseYear = session.ReleaseYear,
                    StartTime = session.StartTime,
                    DurationMinutes = session.DurationMinutes
                });
            }

            return new CinemaHallDetailDto
            {
                Id = hall.Id,
                Name = hall.Name,
                HallType = hall.HallType.ToString(),
                SeatsCount = hall.SeatsCount,
                Sessions = sessionDtos
            };
        }
    }
}
