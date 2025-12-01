using Microsoft.AspNetCore.SignalR;

namespace MeetingRoomTrackerApi.SignalR
{
    public class MeetingRoomHub : Hub
    {
        public async Task UpdateRoomStatus(string message)
        {
            await Clients.All.SendAsync("ReceiveRoomStatusUpdate", message);
        }
        public async Task RoomOccupied(string message)
        {
            await Clients.All.SendAsync("ReceiveRoomOccupied", message);
        }
        public async Task RoomVacated(string message)
        {
            await Clients.All.SendAsync("ReceiveRoomVacated", message);
        }

    }
}
