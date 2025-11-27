using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingRoomTrackerLib.Models;
using MeetingRoomTrackerLib.Repos;

namespace MeetingRoomTrackerLib.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepos<Room> _roomRepo;

        public RoomService(IRepos<Room> rooms) { _roomRepo = rooms; }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomRepo.GetAll();
        }
        public Room GetRoomById(int id)
        {
            Room? room = _roomRepo.GetById(id);
            if (room == null)
            {
                throw new KeyNotFoundException($"Room with ID {id} not found.");
            }
            return room;
        }
        public Room CreateRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            }
            return _roomRepo.Add(room);
        }
        public Room UpdateRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            }
            Room? existingRoom = _roomRepo.GetById(room.Id);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException($"Room with ID {room.Id} not found.");
            }
            return _roomRepo.Update(room, room.Id);
        }

        public Room DeleteRoom(int id)
        {
            Room? existingRoom = _roomRepo.GetById(id);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException($"Room with ID {id} not found.");
            }
            _roomRepo.Delete(id);
            return existingRoom;
        }
        public bool GetStatus(int id)
        {
            Room? room = _roomRepo.GetById(id);
            if (room == null)
            {
                throw new KeyNotFoundException($"Room with ID {id} not found.");
            }
            return room.Status;
        }

    }
}
