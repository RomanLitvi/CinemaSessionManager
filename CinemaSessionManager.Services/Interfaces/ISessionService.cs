using CinemaSessionManager.Models.Enums;
using CinemaSessionManager.Services.Dtos;

namespace CinemaSessionManager.Services.Interfaces
{
    public interface ISessionService
    {
        Task<SessionDetailDto?> GetSessionDetailsAsync(int sessionId);
        Task<SessionDetailDto> CreateSessionAsync(int cinemaHallId, string movieTitle, MovieGenre genre,
            int releaseYear, DateTime startTime, int durationMinutes);
        Task UpdateSessionAsync(int id, string movieTitle, MovieGenre genre,
            int releaseYear, DateTime startTime, int durationMinutes);
        Task DeleteSessionAsync(int id);
    }
}
