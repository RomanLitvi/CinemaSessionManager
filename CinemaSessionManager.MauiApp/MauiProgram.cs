using CinemaSessionManager.Services;
using CinemaSessionManager.MauiApp.Pages;
using CinemaSessionManager.MauiApp.ViewModels;

namespace CinemaSessionManager.MauiApp
{
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

            var dataPath = FileSystem.AppDataDirectory;
            builder.Services.AddCinemaServices(dataPath);

            builder.Services.AddTransient<CinemaHallsListViewModel>();
            builder.Services.AddTransient<CinemaHallDetailsViewModel>();
            builder.Services.AddTransient<SessionDetailsViewModel>();

            builder.Services.AddTransient<CinemaHallsListPage>();
            builder.Services.AddTransient<CinemaHallDetailsPage>();
            builder.Services.AddTransient<SessionDetailsPage>();

            builder.Services.AddSingleton<AppShell>();

            return builder.Build();
        }
    }
}
