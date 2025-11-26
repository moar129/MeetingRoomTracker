using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingRoomTrackerLib.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeetingRoomTrackerLib.EnumClasses;

namespace MeetingRoomTrackerLib.Repos.Tests
{

    [TestClass()]
    public class RoomRepoTests
    {
        private static RoomRepo _repo;
        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<RMTDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // fresh for every test
                .Options;

            var context = new RMTDbContext(options);

            _repo = new RoomRepo(context);

        }
        // Centralized way to create test rooms
        private Room CreateRoom(
            string name = "test",
            RoomTypeEnum roomType = RoomTypeEnum.Mødelokale,
            BuildingEnum building = BuildingEnum.A,
            int floor = 2)
        {
            return new Room
            {
                Name = name,
                RoomType = roomType,
                Building = building,
                Floor = floor
            };
        }


        [TestMethod()]
        public void AddRoomTest()
        {
            var roomToBeAdded = CreateRoom("test");


             _repo.Add(roomToBeAdded);
            
            
            Assert.AreEqual(1, _repo.GetAll().Count());
            Assert.AreEqual(roomToBeAdded.Name, _repo.GetById(1).Name);

        }

        [TestMethod()]
        public void GetAllRoomTest()
        {
            var roomToBeAdded = CreateRoom("Test1");
            var roomToBeAdded2 = CreateRoom("Test2");

            _repo.Add(roomToBeAdded);
            _repo.Add(roomToBeAdded2);

            var rooms = _repo.GetAll();
            Assert.IsNotNull(rooms);
            Assert.AreEqual(2, rooms.Count());
           
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var roomToBeAdded = CreateRoom("test");
            _repo.Add(roomToBeAdded);
            var room = _repo.GetById(1);
            Assert.AreEqual(roomToBeAdded.Name, room.Name);
            
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var roomToBeUpdated = CreateRoom("test");
            _repo.Add(roomToBeUpdated);
            roomToBeUpdated.Name = "test2";
            _repo.Update(roomToBeUpdated, 1);
            var room = _repo.GetById(1);
            Assert.AreEqual(roomToBeUpdated.Name, room.Name);
            
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var roomToBeDelete = CreateRoom("test");
            _repo.Add(roomToBeDelete);
            _repo.Delete(1);
            Assert.AreEqual(0, _repo.GetAll().Count());
            
        }
    }
}