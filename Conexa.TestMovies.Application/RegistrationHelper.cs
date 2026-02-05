using Conexa.TestMovies.Application.BackGroundServices.CronSyncMovies;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Conexa.TestMovies.Application
{
    public static class RegistrationHelper
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddHostedService<CronSyncMovieDaily>();
            return services;
        }
    }
}
