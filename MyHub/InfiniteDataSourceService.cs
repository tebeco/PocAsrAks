using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace MyHub
{
    public class InfiniteDataSourceService : BackgroundService
    {
        private readonly InfiniteDataSource _dataSource;
        public InfiniteDataSourceService(InfiniteDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.Register(() =>
            {
                _dataSource.Stop();
            });

            _dataSource.Start();

            return Task.CompletedTask;
        }
    }
}