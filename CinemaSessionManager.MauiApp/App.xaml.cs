using CinemaSessionManager.MauiApp.Pages;

namespace CinemaSessionManager.MauiApp
{
    /// <summary>
    /// Головний клас застосунку. Визначає початкову сторінку та навігаційну структуру.
    /// partial — бо друга частина класу генерується автоматично з App.xaml (ресурси, стилі).
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            // InitializeComponent() завантажує XAML-розмітку (App.xaml) і створює ресурси (кольори тощо)
            InitializeComponent();

            // NavigationPage — контейнер навігації, який забезпечує стек сторінок і кнопку "Назад".
            // CinemaHallsListPage — перша сторінка, яку бачить користувач.
            MainPage = new NavigationPage(new CinemaHallsListPage())
            {
                BarBackgroundColor = Color.FromArgb("#6366f1"),
                BarTextColor = Colors.White
            };
        }
    }
}
