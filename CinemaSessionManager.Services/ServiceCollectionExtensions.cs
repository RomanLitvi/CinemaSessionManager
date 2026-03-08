using CinemaSessionManager.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSessionManager.Services
{
    /// <summary>
    /// Розширення для реєстрації сервісів у IoC-контейнері.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCinemaServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataStore, FakeDataStore>();
            services.AddTransient<ICinemaService, CinemaService>();
            return services;
        }
    }
}
