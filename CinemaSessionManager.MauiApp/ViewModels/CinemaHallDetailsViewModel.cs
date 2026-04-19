using System.Collections.ObjectModel;
using System.Windows.Input;
using CinemaSessionManager.Models.Enums;
using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.MauiApp.ViewModels
{
    public class CinemaHallDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly ISessionService _sessionService;

        private int _hallId;
        private CinemaHallDetailDto? _hall;
        public CinemaHallDetailDto? Hall
        {
            get => _hall;
            private set => SetField(ref _hall, value);
        }

        private List<SessionListDto> _allSessions = new();
        private ObservableCollection<SessionListDto> _sessions = new();
        public ObservableCollection<SessionListDto> Sessions
        {
            get => _sessions;
            private set => SetField(ref _sessions, value);
        }

        private SessionListDto? _selectedSession;
        public SessionListDto? SelectedSession
        {
            get => _selectedSession;
            set
            {
                if (SetField(ref _selectedSession, value) && value != null)
                {
                    NavigateToSessionDetails(value.Id);
                    SetField(ref _selectedSession, null, nameof(SelectedSession));
                }
            }
        }

        // --- Edit mode ---
        private bool _isEditing;
        public bool IsEditing
        {
            get => _isEditing;
            set
            {
                if (SetField(ref _isEditing, value))
                    OnPropertyChanged(nameof(IsNotEditing));
            }
        }
        public bool IsNotEditing => !IsEditing;

        private string _editName = string.Empty;
        public string EditName { get => _editName; set => SetField(ref _editName, value); }

        private int _editSeatsCount;
        public int EditSeatsCount { get => _editSeatsCount; set => SetField(ref _editSeatsCount, value); }

        private string _editSeatsText = string.Empty;
        public string EditSeatsText
        {
            get => _editSeatsText;
            set
            {
                if (SetField(ref _editSeatsText, value) && int.TryParse(value, out int seats))
                    _editSeatsCount = seats;
            }
        }

        private int _editHallTypeIndex;
        public int EditHallTypeIndex { get => _editHallTypeIndex; set => SetField(ref _editHallTypeIndex, value); }

        public List<string> HallTypeOptions { get; } = Enum.GetValues<CinemaHallType>().Select(t => t.ToString()).ToList();

        // --- Session search & sort ---
        private string _sessionSearchQuery = string.Empty;
        public string SessionSearchQuery
        {
            get => _sessionSearchQuery;
            set
            {
                if (SetField(ref _sessionSearchQuery, value))
                    ApplySessionFilterAndSort();
            }
        }

        private static readonly List<string> SessionSortOptionsList = new()
        {
            "За назвою", "За жанром", "За часом", "За тривалістю"
        };
        public List<string> SessionSortOptions => SessionSortOptionsList;

        private int _sessionSortIndex;
        public int SessionSortIndex
        {
            get => _sessionSortIndex;
            set
            {
                if (SetField(ref _sessionSortIndex, value))
                    ApplySessionFilterAndSort();
            }
        }

        private bool _isSessionAscending = true;
        public bool IsSessionAscending
        {
            get => _isSessionAscending;
            set
            {
                if (SetField(ref _isSessionAscending, value))
                    ApplySessionFilterAndSort();
            }
        }

        // --- Commands ---
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand DeleteHallCommand { get; }
        public ICommand AddSessionCommand { get; }
        public ICommand DeleteSessionCommand { get; }
        public ICommand ToggleSessionSortCommand { get; }

        public CinemaHallDetailsViewModel(ICinemaHallService cinemaHallService, ISessionService sessionService)
        {
            _cinemaHallService = cinemaHallService;
            _sessionService = sessionService;

            EditCommand = new RelayCommand(EnterEditMode);
            SaveCommand = new AsyncRelayCommand(SaveAsync);
            CancelEditCommand = new RelayCommand(CancelEdit);
            DeleteHallCommand = new AsyncRelayCommand(DeleteHallAsync);
            AddSessionCommand = new AsyncRelayCommand(AddSessionAsync);
            DeleteSessionCommand = new AsyncRelayCommand(p => DeleteSessionAsync(p));
            ToggleSessionSortCommand = new RelayCommand(() => IsSessionAscending = !IsSessionAscending);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("hallId", out var value)
                && int.TryParse(value?.ToString(), out int hallId))
            {
                _hallId = hallId;
            }
        }

        public async Task LoadAsync()
        {
            if (_hallId <= 0) return;

            await RunBusyAsync(async () =>
            {
                Hall = await _cinemaHallService.GetHallDetailsAsync(_hallId);
                _allSessions = Hall?.Sessions ?? new List<SessionListDto>();
                ApplySessionFilterAndSort();
            });
        }

        private void ApplySessionFilterAndSort()
        {
            var filtered = _allSessions.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SessionSearchQuery))
            {
                var query = SessionSearchQuery.Trim().ToLowerInvariant();
                filtered = filtered.Where(s =>
                    s.MovieTitle.ToLowerInvariant().Contains(query) ||
                    s.Genre.ToString().ToLowerInvariant().Contains(query));
            }

            filtered = SessionSortIndex switch
            {
                0 => IsSessionAscending
                    ? filtered.OrderBy(s => s.MovieTitle)
                    : filtered.OrderByDescending(s => s.MovieTitle),
                1 => IsSessionAscending
                    ? filtered.OrderBy(s => s.Genre)
                    : filtered.OrderByDescending(s => s.Genre),
                2 => IsSessionAscending
                    ? filtered.OrderBy(s => s.StartTime)
                    : filtered.OrderByDescending(s => s.StartTime),
                3 => IsSessionAscending
                    ? filtered.OrderBy(s => s.DurationMinutes)
                    : filtered.OrderByDescending(s => s.DurationMinutes),
                _ => filtered
            };

            Sessions = new ObservableCollection<SessionListDto>(filtered);
        }

        private void EnterEditMode()
        {
            if (Hall == null) return;
            EditName = Hall.Name;
            EditSeatsCount = Hall.SeatsCount;
            EditSeatsText = Hall.SeatsCount.ToString();
            EditHallTypeIndex = HallTypeOptions.IndexOf(Hall.HallType.ToString());
            IsEditing = true;
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(EditName))
            {
                await Shell.Current.DisplayAlert("Помилка", "Назва залу не може бути порожньою.", "OK");
                return;
            }
            if (_editSeatsCount <= 0)
            {
                await Shell.Current.DisplayAlert("Помилка", "Кількість місць повинна бути більше 0.", "OK");
                return;
            }

            var hallType = Enum.GetValues<CinemaHallType>()[EditHallTypeIndex];

            await RunBusyAsync(async () =>
            {
                await _cinemaHallService.UpdateHallAsync(_hallId, EditName.Trim(), _editSeatsCount, hallType);
                Hall = await _cinemaHallService.GetHallDetailsAsync(_hallId);
                _allSessions = Hall?.Sessions ?? new List<SessionListDto>();
                ApplySessionFilterAndSort();
            });

            IsEditing = false;
        }

        private void CancelEdit()
        {
            IsEditing = false;
        }

        private async Task DeleteHallAsync()
        {
            if (Hall == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Підтвердження",
                $"Видалити кінозал \"{Hall.Name}\" та всі його сеанси?", "Видалити", "Скасувати");
            if (!confirm) return;

            await RunBusyAsync(async () =>
            {
                await _cinemaHallService.DeleteHallAsync(_hallId);
            });

            await Shell.Current.GoToAsync("..");
        }

        private async Task AddSessionAsync()
        {
            await Shell.Current.GoToAsync($"sessiondetails?hallId={_hallId}&isNew=true");
        }

        private async Task DeleteSessionAsync(object? parameter)
        {
            if (!TryParseId(parameter, out int sessionId))
                return;

            var session = _allSessions.FirstOrDefault(s => s.Id == sessionId);
            bool confirm = await Shell.Current.DisplayAlert("Підтвердження",
                $"Видалити сеанс \"{session?.MovieTitle}\"?", "Видалити", "Скасувати");
            if (!confirm) return;

            await RunBusyAsync(async () =>
            {
                await _sessionService.DeleteSessionAsync(sessionId);
                Hall = await _cinemaHallService.GetHallDetailsAsync(_hallId);
                _allSessions = Hall?.Sessions ?? new List<SessionListDto>();
                ApplySessionFilterAndSort();
            });
        }

        private async void NavigateToSessionDetails(int sessionId)
        {
            await Shell.Current.GoToAsync($"sessiondetails?sessionId={sessionId}&hallId={_hallId}");
        }

        private static bool TryParseId(object? value, out int id)
        {
            if (value is int i) { id = i; return true; }
            if (value != null && int.TryParse(value.ToString(), out id)) return true;
            id = 0;
            return false;
        }
    }
}
