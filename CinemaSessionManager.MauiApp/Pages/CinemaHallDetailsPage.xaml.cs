using CinemaSessionManager.MauiApp.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    public partial class CinemaHallDetailsPage : ContentPage
    {
        private readonly CinemaHallDetailsViewModel _viewModel;

        public CinemaHallDetailsPage(CinemaHallDetailsViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadAsync();
        }
    }
}
