using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MyHub
{
    public class ZeHub : Hub
    {
        private readonly ILogger<ZeHub> _logger;

        public ZeHub(ILogger<ZeHub> logger)
        {
            _logger = logger;
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}