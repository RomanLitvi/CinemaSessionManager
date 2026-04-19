using CinemaSessionManager.Repositories.Interfaces;
using CinemaSessionManager.Repositories.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSessionManager.Repositories
{
    public static class RepositoryCollectionExtensions
    {
        public static IServiceCollection AddCinemaRepositories(this IServiceCollection services, string dataDirectoryPath)
        {
            services.AddSingleton<IDataStore>(new JsonDataStore(dataDirectoryPath));
            services.AddTransient<ICinemaHallRepository, CinemaHallRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            return services;
        }
    }
}
