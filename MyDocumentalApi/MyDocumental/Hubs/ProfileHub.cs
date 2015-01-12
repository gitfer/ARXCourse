using Microsoft.AspNet.SignalR;

namespace MyDocumental.Hubs
{
    public class ProfileHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }
    }
}