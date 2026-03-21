using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Repositories.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSessionManager.Repositories
{
    /// <summary>
    /// Розширення для реєстрації репозиторіїв у IoC-контейнері.
    /// </summary>
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddCinemaRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IDataStore, FakeDataStore>();
            services.AddTransient<ICinemaHallRepository, CinemaHallRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            return services;
        }
    }
}
