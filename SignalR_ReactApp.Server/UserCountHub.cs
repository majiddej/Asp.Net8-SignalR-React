using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using System.Threading.Tasks;
namespace SignalR_ReactApp.Server
{
    public class UserCountHub : Hub
    {
        private static int _userCount = 0;

        public override Task OnConnectedAsync()
        {
            _userCount++;
            Clients.All.SendAsync("UpdateUserCount", _userCount);



            //var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //var userName = Context.User?.Identity?.Name;
            // Get user identity (e.g., user ID or username)
            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userName = Context.User?.Identity?.Name;
            if (userName != null)
            {
                Clients.Caller.SendAsync("Welcome", $"Welcome {userName}, you are connected!");
            }
            else
            {
                Clients.Caller.SendAsync("Welcome", $"Anonymous, you are connected!");

            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _userCount--;

            var userId = Context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Clients.All.SendAsync("UpdateUserCount", _userCount);
            return base.OnDisconnectedAsync(exception);
        }
    }

}
