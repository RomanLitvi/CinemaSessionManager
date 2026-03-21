using CinemaSessionManager.Services;
using CinemaSessionManager.MauiApp.Pages;
using CinemaSessionManager.MauiApp.ViewModels;

namespace CinemaSessionManager.MauiApp
{
    /// <summary>
    /// Точка входу MAUI-застосунку. Налаштовує IoC-контейнер
    /// та реєструє всі залежності (репозиторії, сервіси, сторінки, ViewModel-и).
    /// </summary>
    public static class MauiProgram
    {
        public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
        {
            var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Рівень Repository і Service через один виклик (Repository реєструється всередині)
            builder.Services.AddCinemaServices();

            // ViewModels (отримують сервіси через DI)
            builder.Services.AddTransient<CinemaHallsListViewModel>();
            builder.Services.AddTransient<CinemaHallDetailsViewModel>();
            builder.Services.AddTransient<SessionDetailsViewModel>();

            // Сторінки (отримують свої ViewModels через DI)
            builder.Services.AddTransient<CinemaHallsListPage>();
            builder.Services.AddTransient<CinemaHallDetailsPage>();
            builder.Services.AddTransient<SessionDetailsPage>();

            // AppShell (отримує першу сторінку через DI)
            builder.Services.AddSingleton<AppShell>();

            return builder.Build();
        }
    }
}
