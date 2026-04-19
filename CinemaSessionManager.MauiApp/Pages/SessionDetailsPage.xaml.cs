using CinemaSessionManager.MauiApp.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    public partial class SessionDetailsPage : ContentPage
    {
        private readonly SessionDetailsViewModel _viewModel;

        public SessionDetailsPage(SessionDetailsViewModel viewModel)
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
