using Conexa.TestMovies.Domain.Interfaces;
using Conexa.TestMovies.Persistence.Repository;
using Conexa.TestMovies.Persistence.Repository.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Conexa.TestMovies.Persistence
{
    public static class RegistrationHelper
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlite("Data Source=app.db"));
            services.AddTransient(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
