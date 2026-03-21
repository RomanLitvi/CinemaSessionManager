using CinemaSessionManager.Repositories;
using CinemaSessionManager.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSessionManager.Services
{
    /// <summary>
    /// Розширення для реєстрації сервісів і репозиторіїв у IoC-контейнері.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCinemaServices(this IServiceCollection services)
        {
            // Реєстрація рівня репозиторіїв
            services.AddCinemaRepositories();

            // Реєстрація сервісів
            services.AddTransient<ICinemaHallService, CinemaHallService>();
            services.AddTransient<ISessionService, SessionService>();
            return services;
        }
    }
}
