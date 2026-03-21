using CinemaSessionManager.MauiApp.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    /// <summary>
    /// Сторінка деталей кінозалу. Вся логіка знаходиться у CinemaHallDetailsViewModel.
    /// </summary>
    public partial class CinemaHallDetailsPage : ContentPage
    {
        public CinemaHallDetailsPage(CinemaHallDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
