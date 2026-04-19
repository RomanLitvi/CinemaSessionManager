using CinemaSessionManager.Models.Entities;
using CinemaSessionManager.Models.Enums;
using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly ICinemaHallRepository _hallRepository;

        public SessionService(ISessionRepository sessionRepository, ICinemaHallRepository hallRepository)
        {
            _sessionRepository = sessionRepository;
            _hallRepository = hallRepository;
        }

        public async Task<SessionDetailDto?> GetSessionDetailsAsync(int sessionId)
        {
            var session = await _sessionRepository.GetByIdAsync(sessionId);
            if (session == null)
                return null;

            var hall = await _hallRepository.GetByIdAsync(session.CinemaHallId);

            return new SessionDetailDto
            {
                Id = session.Id,
                CinemaHallId = session.CinemaHallId,
                MovieTitle = session.MovieTitle,
                HallName = hall?.Name ?? string.Empty,
                Genre = session.Genre,
                ReleaseYear = session.ReleaseYear,
                StartTime = session.StartTime,
                DurationMinutes = session.DurationMinutes
            };
        }

        public async Task<SessionDetailDto> CreateSessionAsync(int cinemaHallId, string movieTitle,
            MovieGenre genre, int releaseYear, DateTime startTime, int durationMinutes)
        {
            int newId = await _sessionRepository.GenerateNextIdAsync();
            var entity = new SessionEntity(newId, cinemaHallId, movieTitle, genre, releaseYear, startTime, durationMinutes);
            await _sessionRepository.AddAsync(entity);

            var hall = await _hallRepository.GetByIdAsync(cinemaHallId);

            return new SessionDetailDto
            {
                Id = entity.Id,
                CinemaHallId = entity.CinemaHallId,
                MovieTitle = entity.MovieTitle,
                HallName = hall?.Name ?? string.Empty,
                Genre = entity.Genre,
                ReleaseYear = entity.ReleaseYear,
                StartTime = entity.StartTime,
                DurationMinutes = entity.DurationMinutes
            };
        }

        public async Task UpdateSessionAsync(int id, string movieTitle, MovieGenre genre,
            int releaseYear, DateTime startTime, int durationMinutes)
        {
            var existing = await _sessionRepository.GetByIdAsync(id);
            if (existing == null)
                return;

            var updated = new SessionEntity(id, existing.CinemaHallId, movieTitle, genre,
                releaseYear, startTime, durationMinutes);
            await _sessionRepository.UpdateAsync(updated);
        }

        public async Task DeleteSessionAsync(int id)
        {
            await _sessionRepository.DeleteAsync(id);
        }
    }
}
