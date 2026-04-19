using System.Collections.ObjectModel;
using System.Windows.Input;
using CinemaSessionManager.Models.Enums;
using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.MauiApp.ViewModels
{
    public class CinemaHallsListViewModel : BaseViewModel
    {
        private readonly ICinemaHallService _cinemaHallService;

        private List<CinemaHallListDto> _allHalls = new();
        private ObservableCollection<CinemaHallListDto> _halls = new();
        public ObservableCollection<CinemaHallListDto> Halls
        {
            get => _halls;
            private set => SetField(ref _halls, value);
        }

        private CinemaHallListDto? _selectedHall;
        public CinemaHallListDto? SelectedHall
        {
            get => _selectedHall;
            set
            {
                if (SetField(ref _selectedHall, value) && value != null)
                {
                    NavigateToHallDetails(value.Id);
                    SetField(ref _selectedHall, null, nameof(SelectedHall));
                }
            }
        }

        private string _searchQuery = string.Empty;
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (SetField(ref _searchQuery, value))
                    ApplyFilterAndSort();
            }
        }

        private static readonly List<string> SortOptionsList = new()
        {
            "За назвою", "За типом", "За місцями", "За к-стю сеансів"
        };

        public List<string> SortOptions => SortOptionsList;

        private int _selectedSortIndex;
        public int SelectedSortIndex
        {
            get => _selectedSortIndex;
            set
            {
                if (SetField(ref _selectedSortIndex, value))
                    ApplyFilterAndSort();
            }
        }

        private bool _isAscending = true;
        public bool IsAscending
        {
            get => _isAscending;
            set
            {
                if (SetField(ref _isAscending, value))
                    ApplyFilterAndSort();
            }
        }

        public ICommand AddHallCommand { get; }
        public ICommand DeleteHallCommand { get; }
        public ICommand ToggleSortDirectionCommand { get; }

        public CinemaHallsListViewModel(ICinemaHallService cinemaHallService)
        {
            _cinemaHallService = cinemaHallService;

            AddHallCommand = new AsyncRelayCommand(AddHallAsync);
            DeleteHallCommand = new AsyncRelayCommand(p => DeleteHallAsync(p));
            ToggleSortDirectionCommand = new RelayCommand(() => IsAscending = !IsAscending);
        }

        public async Task LoadHallsAsync()
        {
            await RunBusyAsync(async () =>
            {
                _allHalls = await _cinemaHallService.GetAllHallsAsync();
                ApplyFilterAndSort();
            });
        }

        private void ApplyFilterAndSort()
        {
            var filtered = _allHalls.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                var query = SearchQuery.Trim().ToLowerInvariant();
                filtered = filtered.Where(h =>
                    h.Name.ToLowerInvariant().Contains(query) ||
                    h.HallType.ToString().ToLowerInvariant().Contains(query));
            }

            filtered = SelectedSortIndex switch
            {
                0 => IsAscending ? filtered.OrderBy(h => h.Name) : filtered.OrderByDescending(h => h.Name),
                1 => IsAscending ? filtered.OrderBy(h => h.HallType) : filtered.OrderByDescending(h => h.HallType),
                2 => IsAscending ? filtered.OrderBy(h => h.SeatsCount) : filtered.OrderByDescending(h => h.SeatsCount),
                3 => IsAscending ? filtered.OrderBy(h => h.SessionCount) : filtered.OrderByDescending(h => h.SessionCount),
                _ => filtered
            };

            Halls = new ObservableCollection<CinemaHallListDto>(filtered);
        }

        private async Task AddHallAsync()
        {
            string? name = await Shell.Current.DisplayPromptAsync("Новий кінозал", "Назва залу:");
            if (string.IsNullOrWhiteSpace(name))
                return;

            string? seatsStr = await Shell.Current.DisplayPromptAsync("Новий кінозал", "Кількість місць:",
                keyboard: Keyboard.Numeric);
            if (!int.TryParse(seatsStr, out int seats) || seats <= 0)
            {
                await Shell.Current.DisplayAlert("Помилка", "Введіть коректну кількість місць.", "OK");
                return;
            }

            var hallTypes = Enum.GetValues<CinemaHallType>();
            string action = await Shell.Current.DisplayActionSheet("Тип залу", "Скасувати", null,
                hallTypes.Select(t => t.ToString()).ToArray());
            if (string.IsNullOrEmpty(action) || action == "Скасувати")
                return;

            var hallType = Enum.Parse<CinemaHallType>(action);

            await RunBusyAsync(async () =>
            {
                await _cinemaHallService.CreateHallAsync(name.Trim(), seats, hallType);
                _allHalls = await _cinemaHallService.GetAllHallsAsync();
                ApplyFilterAndSort();
            });
        }

        private async Task DeleteHallAsync(object? parameter)
        {
            if (!TryParseId(parameter, out int hallId))
                return;

            var hall = _allHalls.FirstOrDefault(h => h.Id == hallId);
            bool confirm = await Shell.Current.DisplayAlert("Підтвердження",
                $"Видалити кінозал \"{hall?.Name}\" та всі його сеанси?", "Видалити", "Скасувати");
            if (!confirm)
                return;

            await RunBusyAsync(async () =>
            {
                await _cinemaHallService.DeleteHallAsync(hallId);
                _allHalls = await _cinemaHallService.GetAllHallsAsync();
                ApplyFilterAndSort();
            });
        }

        private async void NavigateToHallDetails(int hallId)
        {
            await Shell.Current.GoToAsync($"halldetails?hallId={hallId}");
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
