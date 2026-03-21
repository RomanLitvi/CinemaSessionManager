using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.Services
{
    /// <summary>
    /// Сервіс для роботи з кіносеансами.
    /// Отримує дані з репозиторіїв, конвертує Entity → DTO і повертає для використання у UI.
    /// </summary>
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ICinemaHallRepository _hallRepository;

        public SessionService(ISessionRepository sessionRepository, ICinemaHallRepository hallRepository)
        {
            _sessionRepository = sessionRepository;
            _hallRepository = hallRepository;
        }

        /// <summary>
        /// Повертає детальну інформацію про кіносеанс разом з назвою кінозалу.
        /// </summary>
        public SessionDetailDto? GetSessionDetails(int sessionId)
        {
            var session = _sessionRepository.GetById(sessionId);
            if (session == null)
                return null;

            var hall = _hallRepository.GetById(session.CinemaHallId);

            return new SessionDetailDto
            {
                Id = session.Id,
                MovieTitle = session.MovieTitle,
                HallName = hall?.Name ?? string.Empty,
                Genre = session.Genre.ToString(),
                ReleaseYear = session.ReleaseYear,
                StartTime = session.StartTime,
                DurationMinutes = session.DurationMinutes
            };
        }
    }
}
