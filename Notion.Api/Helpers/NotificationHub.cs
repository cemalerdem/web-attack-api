using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Notion.Api.Helpers
{
    public class NotificationHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}