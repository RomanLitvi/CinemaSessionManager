using CinemaSessionManager.Services.Interfaces;
using CinemaSessionManager.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    /// <summary>
    /// Code-Behind для сторінки списку кінозалів.
    /// Це "логіка" сторінки — завантажує дані та обробляє дії користувача.
    /// Пов'язана з CinemaHallsListPage.xaml через partial class та InitializeComponent().
    /// </summary>
    public partial class CinemaHallsListPage : ContentPage
    {
        private readonly ICinemaService _cinemaService;

        public CinemaHallsListPage()
        {
            // Завантажує UI-розмітку з .xaml файлу та створює всі елементи інтерфейсу
            InitializeComponent();

            // Отримуємо сервіс з IoC-контейнера замість створення через new (Dependency Injection)
            _cinemaService = Handler?.MauiContext?.Services.GetRequiredService<ICinemaService>()
                ?? IPlatformApplication.Current!.Services.GetRequiredService<ICinemaService>();

            LoadCinemaHalls();
        }

        /// <summary>
        /// Завантажує список кінозалів із сервісу та прив'язує до CollectionView.
        /// ItemsSource — це властивість UI-елемента, яка визначає джерело даних для списку.
        /// </summary>
        private void LoadCinemaHalls()
        {
            var halls = _cinemaService.GetAllCinemaHalls();

            foreach (var hall in halls)
            {
                _cinemaService.LoadSessions(hall);
            }

            // Прив'язка даних: CollectionView бере цей список і для кожного елемента
            // створює UI за шаблоном DataTemplate, визначеним у .xaml файлі
            HallsCollectionView.ItemsSource = halls;
        }

        /// <summary>
        /// Обробник події вибору елемента у списку.
        /// Ця подія вказана в .xaml: SelectionChanged="HallsCollectionView_SelectionChanged"
        /// </summary>
        private async void HallsCollectionView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is CinemaHallViewModel selectedHall)
            {
                HallsCollectionView.SelectedItem = null;
                // Navigation.PushAsync — додає нову сторінку поверх поточної (стек навігації)
                await Navigation.PushAsync(new CinemaHallDetailsPage(selectedHall.Id));
            }
        }
    }
}
