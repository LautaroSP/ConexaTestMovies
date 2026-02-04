using Conexa.TestMovies.ApiClients.StarWars;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Conexa.TestMovies.ApiClients
{
    public static class RegistrationHelper
    {
        public static IServiceCollection AddApiClientsServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRefitClient<IStarWarsAPI>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://www.swapi.tech"));
            return services;
        }
    }
}
