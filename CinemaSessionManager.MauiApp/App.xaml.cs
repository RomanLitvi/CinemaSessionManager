namespace CinemaSessionManager.MauiApp
{
    /// <summary>
    /// Головний клас застосунку. Отримує AppShell через DI і встановлює як головну сторінку.
    /// </summary>
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            UserAppTheme = AppTheme.Light;
            MainPage = serviceProvider.GetRequiredService<AppShell>();
        }
    }
}
