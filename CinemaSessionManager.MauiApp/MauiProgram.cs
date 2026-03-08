using CinemaSessionManager.Services;

namespace CinemaSessionManager.MauiApp
{
    /// <summary>
    /// Точка входу MAUI-застосунку. Тут налаштовується IoC-контейнер,
    /// реєструються сервіси та конфігурується застосунок.
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

            // Реєстрація сервісів через IoC-контейнер (Dependency Injection).
            // AddCinemaServices() — метод розширення, який реєструє IDataStore та ICinemaService.
            builder.Services.AddCinemaServices();

            return builder.Build();
        }
    }
}
