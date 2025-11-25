using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingRoomTrackerLib.Models;
using MeetingRoomTrackerLib;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomTrackerLib.Repos
{
    public class RoomRepo
    {
        private RMTDbContext _context;
        public RoomRepo(RMTDbContext Context)
        {
            _context = Context;
        }
        public IEnumerable<Room> GetAll()
        {
            IEnumerable<Room> rooms = _context.Rooms.ToList();
            return rooms;
        }

        public Room? GetById(int id)
        {
            Room? room = _context.Rooms.FirstOrDefault(a => a.Id == id);
            return room;
        }

        public Room? Update(Room roomToBeUpdated, int id)
        {
            Room? existingRoom = GetById(id);
            if (existingRoom != null)
            {
                existingRoom.Name = roomToBeUpdated.Name;
                existingRoom.RoomType = roomToBeUpdated.RoomType;
                existingRoom.Building = roomToBeUpdated.Building;
                existingRoom.Floor = roomToBeUpdated.Floor;
                _context.SaveChanges();
                
            }
            return existingRoom;
        }

        public Room Delete(int id)
        {
            Room? roomToBeDeleted = GetById(id);
            if (roomToBeDeleted != null)
            {
                _context.Rooms.Remove(roomToBeDeleted);
                _context.SaveChanges();
            }
            return roomToBeDeleted!;
        }
    }
}
