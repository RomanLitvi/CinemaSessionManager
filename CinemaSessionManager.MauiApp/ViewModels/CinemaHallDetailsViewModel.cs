using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.MauiApp.ViewModels
{
    /// <summary>
    /// ViewModel для сторінки деталей кінозалу.
    /// Отримує hallId через Shell query-параметри і завантажує деталі залу.
    /// </summary>
    public class CinemaHallDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ICinemaHallService _cinemaHallService;

        private CinemaHallDetailDto? _hall;

        /// <summary>
        /// Детальна інформація про кінозал разом зі списком сеансів.
        /// </summary>
        public CinemaHallDetailDto? Hall
        {
            get => _hall;
            private set => SetField(ref _hall, value);
        }

        private SessionListDto? _selectedSession;

        /// <summary>
        /// Обраний сеанс. При встановленні виконує навігацію на сторінку деталей сеансу.
        /// </summary>
        public SessionListDto? SelectedSession
        {
            get => _selectedSession;
            set
            {
                if (SetField(ref _selectedSession, value) && value != null)
                {
                    NavigateToSessionDetails(value.Id);
                    // Скидаємо виділення після навігації
                    SetField(ref _selectedSession, null, nameof(SelectedSession));
                }
            }
        }

        public CinemaHallDetailsViewModel(ICinemaHallService cinemaHallService)
        {
            _cinemaHallService = cinemaHallService;
        }

        /// <summary>
        /// Викликається Shell-ом при навігації для передачі query-параметрів.
        /// </summary>
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("hallId", out var value)
                && int.TryParse(value?.ToString(), out int hallId))
            {
                Hall = _cinemaHallService.GetHallDetails(hallId);
            }
        }

        private async void NavigateToSessionDetails(int sessionId)
        {
            await Shell.Current.GoToAsync($"sessiondetails?sessionId={sessionId}");
        }
    }
}
