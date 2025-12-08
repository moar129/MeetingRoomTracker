using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomTrackerLib.Services
{
    public interface IRoomService
    {
        IEnumerable<Room> GetAllRooms();
        Room GetRoomById(int id);
        Room CreateRoom(Room room);
        Task<Room> UpdateRoom(Room room);
        Room DeleteRoom(int id);
        bool GetStatus(int id);
    }
}
