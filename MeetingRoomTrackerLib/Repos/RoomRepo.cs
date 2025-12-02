using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingRoomTrackerLib.Models;
using MeetingRoomTrackerLib;
using Microsoft.EntityFrameworkCore;
using MeetingRoomTrackerLib.Services;

namespace MeetingRoomTrackerLib.Repos
{
    public class RoomRepo : IRepos<Room>
    {
        private RMTDbContext _context;
        public RoomRepo(RMTDbContext Context)
        {
            _context = Context;

            RoomsSeedData(); // Seed data if the Rooms table is empty


        }

        public Room Add(Room roomToBeCreated)
        {
            _context.Rooms.Add(roomToBeCreated);
            _context.SaveChanges();
            return roomToBeCreated;
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

        private void RoomsSeedData()
        {
            if (_context.Rooms.Any())
                return;

            var mockRooms = new List<Room>
            {
                new Room { Status = false, RoomType = 0, Name = "London", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 4 },
                new Room { Status = false, RoomType = 0, Name = "Dublin", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 5 },
                new Room { Status = false, RoomType = 0, Name = "Berlin", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 6 },
                new Room { Status = false, RoomType = 0, Name = "Amsterdam", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 7 },
                new Room { Status = false, RoomType = 0, Name = "Brussels", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 8 },
                new Room { Status = false, RoomType = 0, Name = "Luxembourg", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 9 },
                new Room { Status = false, RoomType = 0, Name = "Paris", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 11 },
                new Room { Status = false, RoomType = 0, Name = "Madrid", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 12 },
                new Room { Status = false, RoomType = 0, Name = "Lisbon", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 13 },
                new Room { Status = false, RoomType = 0, Name = "Bern", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 14 },
                new Room { Status = false, RoomType = 0, Name = "Vienna", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 15 },
                new Room { Status = false, RoomType = 0, Name = "Rome", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 16 },
                new Room { Status = false, RoomType = 0, Name = "Stockholm", Building = (EnumClasses.BuildingEnum)3, Floor = 2, RoomNumber = 17 },

                new Room { Status = false, RoomType = 0, Name = "Barcelona", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 4 },
                new Room { Status = false, RoomType = 0, Name = "Valencia", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 5 },
                new Room { Status = false, RoomType = 0, Name = "Seville", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 6 },
                new Room { Status = false, RoomType = 0, Name = "Naples", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 7 },
                new Room { Status = false, RoomType = 0, Name = "Milan", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 8 },
                new Room { Status = false, RoomType = 0, Name = "Turin", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 9 },
                new Room { Status = false, RoomType = 0, Name = "Porto", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 11 },
                new Room { Status = false, RoomType = 0, Name = "Thessaloniki", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 12 },
                new Room { Status = false, RoomType = 0, Name = "Patras", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 13 },
                new Room { Status = false, RoomType = 0, Name = "Split", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 14 },
                new Room { Status = false, RoomType = 0, Name = "Dubrovnik", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 15 },
                new Room { Status = false, RoomType = 0, Name = "Palermo", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 16 },
                new Room { Status = false, RoomType = 0, Name = "Catania", Building = (EnumClasses.BuildingEnum)3, Floor = 3, RoomNumber = 17 },

                new Room { Status = false, RoomType = 0, Name = "Copenhagen", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 1 },
                new Room { Status = false, RoomType = 0, Name = "Oslo", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 2 },
                new Room { Status = false, RoomType = 0, Name = "Helsinki", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 3 },
                new Room { Status = false, RoomType = 0, Name = "Warsaw", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 4 },
                new Room { Status = false, RoomType = 0, Name = "Prague", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 6 },
                new Room { Status = false, RoomType = 0, Name = "Budapest", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 7 },
                new Room { Status = false, RoomType = 0, Name = "Athens", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 8 },
                new Room { Status = false, RoomType = 0, Name = "Zagreb", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 10 },
                new Room { Status = false, RoomType = 0, Name = "Ljubljana", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 12 },
                new Room { Status = false, RoomType = 0, Name = "Sofia", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 13 },
                new Room { Status = false, RoomType = 0, Name = "Bucharest", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 14 },
                new Room { Status = false, RoomType = 0, Name = "Belgrade", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 15 },
                new Room { Status = false, RoomType = 0, Name = "Sarajevo", Building = (EnumClasses.BuildingEnum)1, Floor = 2, RoomNumber = 16 }
            };

            _context.Rooms.AddRange(mockRooms);
            _context.SaveChanges();
        }
    }
}
