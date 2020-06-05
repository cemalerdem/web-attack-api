using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Notion.Api.Helpers
{
    public class ChatHub : Hub
    {
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
    }
}