using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MyHub
{
    public class InfiniteDataSource : IAsyncDisposable
    {
        private readonly Timer _timer;
        private readonly IHubContext<ZeHub> _hub;

        public InfiniteDataSource(IHubContext<ZeHub> hub)
        {
            _timer = new Timer(Tick, new RandomDataGenerator(10f), Timeout.Infinite, Timeout.Infinite);
            _hub = hub;
        }

        public ValueTask DisposeAsync()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
            return _timer.DisposeAsync();
        }

        public void Start()
        {
            _timer.Change(TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(3));
        }

        public void Stop()
        {
            _timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private async void Tick(object state)
        {
            if (state is RandomDataGenerator generator)
            {
                await TickAsync(generator);
            }
        }

        private async Task TickAsync(RandomDataGenerator generator)
        {
            var next = generator.Next();
            await _hub.Clients.All.SendAsync("Data", next);
        }
    }
}
