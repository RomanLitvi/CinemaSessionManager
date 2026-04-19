using CinemaSessionManager.MauiApp.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    public partial class CinemaHallsListPage : ContentPage
    {
        private readonly CinemaHallsListViewModel _viewModel;

        public CinemaHallsListPage(CinemaHallsListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadHallsAsync();
        }
    }
}
