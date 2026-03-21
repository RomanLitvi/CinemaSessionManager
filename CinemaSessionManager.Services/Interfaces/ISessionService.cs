using CinemaSessionManager.Services.Dtos;

namespace CinemaSessionManager.Services.Interfaces
{
    /// <summary>
    /// Інтерфейс сервісу для роботи з кіносеансами.
    /// </summary>
    public interface ISessionService
    {
        SessionDetailDto? GetSessionDetails(int sessionId);
    }
}
