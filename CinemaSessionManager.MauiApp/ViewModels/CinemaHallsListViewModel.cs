using System.Collections.ObjectModel;
using CinemaSessionManager.Services.Dtos;
using CinemaSessionManager.Services.Interfaces;

namespace CinemaSessionManager.MauiApp.ViewModels
{
    /// <summary>
    /// ViewModel для сторінки списку кінозалів.
    /// Завантажує список залів та керує навігацією до деталей залу.
    /// </summary>
    public class CinemaHallsListViewModel : BaseViewModel
    {
        private readonly ICinemaHallService _cinemaHallService;

        private ObservableCollection<CinemaHallListDto> _halls = new ObservableCollection<CinemaHallListDto>();

        /// <summary>
        /// Список кінозалів для відображення у CollectionView.
        /// </summary>
        public ObservableCollection<CinemaHallListDto> Halls
        {
            get => _halls;
            private set => SetField(ref _halls, value);
        }

        private CinemaHallListDto? _selectedHall;

        /// <summary>
        /// Обраний зал. При встановленні виконує навігацію на сторінку деталей.
        /// </summary>
        public CinemaHallListDto? SelectedHall
        {
            get => _selectedHall;
            set
            {
                if (SetField(ref _selectedHall, value) && value != null)
                {
                    NavigateToHallDetails(value.Id);
                    // Скидаємо виділення після навігації
                    SetField(ref _selectedHall, null, nameof(SelectedHall));
                }
            }
        }

        public CinemaHallsListViewModel(ICinemaHallService cinemaHallService)
        {
            _cinemaHallService = cinemaHallService;
            LoadHalls();
        }

        private void LoadHalls()
        {
            var halls = _cinemaHallService.GetAllHalls();
            Halls = new ObservableCollection<CinemaHallListDto>(halls);
        }

        private async void NavigateToHallDetails(int hallId)
        {
            await Shell.Current.GoToAsync($"halldetails?hallId={hallId}");
        }
    }
}
