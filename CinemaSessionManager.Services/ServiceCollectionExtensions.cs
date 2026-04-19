using CinemaSessionManager.Repositories;
using CinemaSessionManager.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSessionManager.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCinemaServices(this IServiceCollection services, string dataDirectoryPath)
        {
            services.AddCinemaRepositories(dataDirectoryPath);
            services.AddTransient<ICinemaHallService, CinemaHallService>();
            services.AddTransient<ISessionService, SessionService>();
            return services;
        }
    }
}
