using CinemaSessionManager.MauiApp.ViewModels;

namespace CinemaSessionManager.MauiApp.Pages
{
    /// <summary>
    /// Сторінка списку кінозалів. Вся логіка знаходиться у CinemaHallsListViewModel.
    /// </summary>
    public partial class CinemaHallsListPage : ContentPage
    {
        public CinemaHallsListPage(CinemaHallsListViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
