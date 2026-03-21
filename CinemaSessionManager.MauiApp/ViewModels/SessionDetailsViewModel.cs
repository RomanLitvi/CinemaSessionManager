using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.MauiApp.ViewModels
{
    /// <summary>
    /// ViewModel для сторінки деталей кіносеансу.
    /// Отримує sessionId через Shell query-параметри і завантажує деталі сеансу.
    /// </summary>
    public class SessionDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ISessionService _sessionService;

        private SessionDetailDto? _session;

        /// <summary>
        /// Детальна інформація про кіносеанс.
        /// </summary>
        public SessionDetailDto? Session
        {
            get => _session;
            private set => SetField(ref _session, value);
        }

        public SessionDetailsViewModel(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        /// <summary>
        /// Викликається Shell-ом при навігації для передачі query-параметрів.
        /// </summary>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("sessionId", out var value)
                && int.TryParse(value?.ToString(), out int sessionId))
            {
                Session = _sessionService.GetSessionDetails(sessionId);
            }
        }
    }
}
