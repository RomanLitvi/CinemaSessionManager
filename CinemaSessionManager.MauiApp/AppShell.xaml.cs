namespace CinemaSessionManager.MauiApp
{
    /// <summary>
    /// Головний Shell застосунку. Визначає навігаційну структуру та реєструє маршрути.
    /// Отримує першу сторінку через DI для коректної ін'єкції залежностей.
    /// </summary>
    public class AppShell : Shell
    {
        public AppShell()
        {
            Shell.SetBackgroundColor(this, Color.FromArgb("#6366f1"));
            Shell.SetForegroundColor(this, Colors.White);
            Shell.SetTitleColor(this, Colors.White);

            // ContentTemplate — сторінка створюється лінива через DI після завантаження App.xaml ресурсів
            Items.Add(new ShellContent
            {
                Route = "halls",
                ContentTemplate = new DataTemplate(typeof(Pages.CinemaHallsListPage))
            });

            // Реєстрація маршрутів для навігації між сторінками
            Routing.RegisterRoute("halldetails", typeof(Pages.CinemaHallDetailsPage));
            Routing.RegisterRoute("sessiondetails", typeof(Pages.SessionDetailsPage));
        }
    }
}
