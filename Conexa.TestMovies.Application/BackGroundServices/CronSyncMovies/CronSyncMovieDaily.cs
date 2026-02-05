using Conexa.TestMovies.Application.Features.Commands.SyncMoviesWithApiClient;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Conexa.TestMovies.Application.BackGroundServices.CronSyncMovies
{
    public class CronSyncMovieDaily : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CronSyncMovieDaily(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var nextRun = DateTime.Now.AddDays(1);
                if (DateTime.Now > nextRun)
                    nextRun = nextRun.AddDays(1);

                await Task.Delay(nextRun - DateTime.Now, stoppingToken);

                using var scope = _scopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                try
                {
                    await mediator.Send(new SyncMoviesWithApiClientCommand(), stoppingToken);
                }
                catch (Exception ex)
                {
                    // loggear error acá
                }
            }
        }
    }
}
