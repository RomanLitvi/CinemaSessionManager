using CinemaSessionManager.MauiApp.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    /// <summary>
    /// Сторінка деталей кіносеансу. Вся логіка знаходиться у SessionDetailsViewModel.
    /// </summary>
    public partial class SessionDetailsPage : ContentPage
    {
        public SessionDetailsPage(SessionDetailsViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
