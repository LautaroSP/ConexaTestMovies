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
                try
                {
                    var now = DateTime.Now;
                    var nextRun = new DateTime(now.Year, now.Month, now.Day, 3, 0, 0); 

                    if (now > nextRun)
                        nextRun = nextRun.AddDays(1);

                    var delay = nextRun - now;
                    await Task.Delay(delay, stoppingToken);

                    using var scope = _scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    await mediator.Send(new SyncMoviesWithApiClientCommand(), stoppingToken);
                }
                catch (TaskCanceledException)
                {
                }
                catch (Exception ex)
                {
                }
            }
        }

    }
}
