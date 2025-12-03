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
        private static IRepos<Room> _repo;
        [TestInitialize]
        public void TestInitialize()
        {
            // (UseInMemoryDatabase) In-memory database setup for testing purposes. This tells EF Core to use its InMemory provider instead of a real SQL database.
            // (databaseName: Guid.NewGuid().ToString()) This generates a unique database name every time using a new GUID.
            // after use of the test, the in-memory database is discarded, ensuring that each test runs in isolation without any leftover data from previous tests.
            var options = new DbContextOptionsBuilder<RMTDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new RMTDbContext(options);

            _repo = new RoomRepo(context);

        }
        // Centralized way to create test rooms
        private Room CreateRoom(
            string name = "test",
            RoomTypeEnum roomType = RoomTypeEnum.Mødelokale,
            BuildingEnum building = BuildingEnum.A,
            int floor = 2, int roomNumber = 2)
        {
            return new Room
            {
                Name = name,
                RoomType = roomType,
                Building = building,
                Floor = floor,
                RoomNumber = roomNumber
            };
        }


        [TestMethod()]
        public void AddRoomTest()
        {
            var roomToBeAdded = CreateRoom("test");


             _repo.Add(roomToBeAdded);
            
            
            Assert.AreEqual(roomToBeAdded.Id, _repo.GetAll().Count());
            Assert.AreEqual(roomToBeAdded.Name, _repo.GetById(roomToBeAdded.Id).Name);

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
            Assert.AreEqual(roomToBeAdded2.Id, rooms.Count());
           
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var roomToBeAdded = CreateRoom("test");
            _repo.Add(roomToBeAdded);
            var room = _repo.GetById(roomToBeAdded.Id);
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
            _repo.Delete(roomToBeDelete.Id);
            Assert.AreEqual(--roomToBeDelete.Id, _repo.GetAll().Count());
            
        }
    }
}