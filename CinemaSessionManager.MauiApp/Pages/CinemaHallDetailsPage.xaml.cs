using CinemaSessionManager.Services.Interfaces;
using CinemaSessionManager.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    /// <summary>
    /// Code-Behind для сторінки деталей кінозалу.
    /// Показує інформацію про зал та список його сеансів.
    /// </summary>
    public partial class CinemaHallDetailsPage : ContentPage
    {
        private readonly ICinemaService _cinemaService;
        private CinemaHallViewModel? _hall;

        public CinemaHallDetailsPage(int hallId)
        {
            InitializeComponent();

            _cinemaService = IPlatformApplication.Current!.Services.GetRequiredService<ICinemaService>();

            LoadHallDetails(hallId);
        }

        /// <summary>
        /// Завантажує деталі кінозалу та заповнює UI-елементи даними.
        /// Значення присвоюються напряму через x:Name — це Code-Behind підхід.
        /// </summary>
        private void LoadHallDetails(int hallId)
        {
            _hall = _cinemaService.GetCinemaHallWithSessions(hallId);

            if (_hall == null)
            {
                DisplayAlert("Помилка", $"Кінозал з ID {hallId} не знайдено.", "OK");
                Navigation.PopAsync();
                return;
            }

            // Присвоєння даних елементам UI за їхніми іменами (x:Name у XAML)
            Title = _hall.Name;
            HallNameLabel.Text = $"🏛 {_hall.Name}";
            HallTypeLabel.Text = _hall.HallType.ToString();
            SeatsCountLabel.Text = _hall.SeatsCount.ToString();
            SessionsCountLabel.Text = _hall.Sessions.Count.ToString();
            TotalDurationLabel.Text = $"{_hall.TotalSessionsDurationMinutes} хв";

            SessionsCollectionView.ItemsSource = _hall.Sessions;
        }

        /// <summary>
        /// При виборі сеансу — перехід на сторінку деталей сеансу (додається в стек навігації).
        /// </summary>
        private async void SessionsCollectionView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is SessionViewModel selectedSession && _hall != null)
            {
                SessionsCollectionView.SelectedItem = null;
                await Navigation.PushAsync(new SessionDetailsPage(selectedSession, _hall.Name));
            }
        }
    }
}
