using System.Windows.Input;
using CinemaSessionManager.Models.Enums;
using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.MauiApp.ViewModels
{
    public class SessionDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly ISessionService _sessionService;

        private int _sessionId;
        private int _hallId;
        private bool _isNewSession;

        private SessionDetailDto? _session;
        public SessionDetailDto? Session
        {
            get => _session;
            private set => SetField(ref _session, value);
        }

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

        private string _editMovieTitle = string.Empty;
        public string EditMovieTitle { get => _editMovieTitle; set => SetField(ref _editMovieTitle, value); }

        private int _editGenreIndex;
        public int EditGenreIndex { get => _editGenreIndex; set => SetField(ref _editGenreIndex, value); }

        private string _editReleaseYearText = string.Empty;
        private int _editReleaseYear;
        public string EditReleaseYearText
        {
            get => _editReleaseYearText;
            set
            {
                if (SetField(ref _editReleaseYearText, value) && int.TryParse(value, out int year))
                    _editReleaseYear = year;
            }
        }

        private TimeSpan _editStartTime;
        public TimeSpan EditStartTime { get => _editStartTime; set => SetField(ref _editStartTime, value); }

        private string _editDurationText = string.Empty;
        private int _editDuration;
        public string EditDurationText
        {
            get => _editDurationText;
            set
            {
                if (SetField(ref _editDurationText, value) && int.TryParse(value, out int d))
                    _editDuration = d;
            }
        }

        public List<string> GenreOptions { get; } = Enum.GetValues<MovieGenre>().Select(g => g.ToString()).ToList();

        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand CancelEditCommand { get; }
        public ICommand DeleteCommand { get; }

        public SessionDetailsViewModel(ISessionService sessionService)
        {
            _sessionService = sessionService;

            EditCommand = new RelayCommand(EnterEditMode);
            SaveCommand = new AsyncRelayCommand(SaveAsync);
            CancelEditCommand = new AsyncRelayCommand(CancelEditAsync);
            DeleteCommand = new AsyncRelayCommand(DeleteAsync);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.TryGetValue("sessionId", out var value)
                && int.TryParse(value?.ToString(), out int sessionId))
            {
                _sessionId = sessionId;
            }
            if (query.TryGetValue("hallId", out var hv)
                && int.TryParse(hv?.ToString(), out int hallId))
            {
                _hallId = hallId;
            }
            _isNewSession = query.ContainsKey("isNew");
        }

        public async Task LoadAsync()
        {
            if (_isNewSession)
            {
                EditMovieTitle = string.Empty;
                EditGenreIndex = 0;
                _editReleaseYear = DateTime.Now.Year;
                EditReleaseYearText = DateTime.Now.Year.ToString();
                EditStartTime = new TimeSpan(12, 0, 0);
                _editDuration = 120;
                EditDurationText = "120";
                IsEditing = true;
                return;
            }

            if (_sessionId <= 0) return;

            await RunBusyAsync(async () =>
            {
                Session = await _sessionService.GetSessionDetailsAsync(_sessionId);
            });
        }

        private void EnterEditMode()
        {
            if (Session == null) return;

            EditMovieTitle = Session.MovieTitle;
            EditGenreIndex = GenreOptions.IndexOf(Session.Genre.ToString());
            _editReleaseYear = Session.ReleaseYear;
            EditReleaseYearText = Session.ReleaseYear.ToString();
            EditStartTime = Session.StartTime.TimeOfDay;
            _editDuration = Session.DurationMinutes;
            EditDurationText = Session.DurationMinutes.ToString();

            IsEditing = true;
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(EditMovieTitle))
            {
                await Shell.Current.DisplayAlert("Помилка", "Назва фільму не може бути порожньою.", "OK");
                return;
            }
            if (_editReleaseYear < 1888)
            {
                await Shell.Current.DisplayAlert("Помилка", "Введіть коректний рік випуску.", "OK");
                return;
            }
            if (_editDuration <= 0)
            {
                await Shell.Current.DisplayAlert("Помилка", "Тривалість повинна бути більше 0.", "OK");
                return;
            }

            var genre = Enum.GetValues<MovieGenre>()[EditGenreIndex];
            var startTime = DateTime.Today.Add(EditStartTime);

            if (_isNewSession)
            {
                await RunBusyAsync(async () =>
                {
                    var created = await _sessionService.CreateSessionAsync(
                        _hallId, EditMovieTitle.Trim(), genre, _editReleaseYear, startTime, _editDuration);
                    _sessionId = created.Id;
                    _isNewSession = false;
                    Session = created;
                });
                IsEditing = false;
            }
            else
            {
                await RunBusyAsync(async () =>
                {
                    await _sessionService.UpdateSessionAsync(_sessionId, EditMovieTitle.Trim(),
                        genre, _editReleaseYear, startTime, _editDuration);
                    Session = await _sessionService.GetSessionDetailsAsync(_sessionId);
                });
                IsEditing = false;
            }
        }

        private async Task CancelEditAsync()
        {
            if (_isNewSession)
            {
                await Shell.Current.GoToAsync("..");
                return;
            }
            IsEditing = false;
        }

        private async Task DeleteAsync()
        {
            if (Session == null) return;

            bool confirm = await Shell.Current.DisplayAlert("Підтвердження",
                $"Видалити сеанс \"{Session.MovieTitle}\"?", "Видалити", "Скасувати");
            if (!confirm) return;

            await RunBusyAsync(async () =>
            {
                await _sessionService.DeleteSessionAsync(_sessionId);
            });

            await Shell.Current.GoToAsync("..");
        }
    }
}
