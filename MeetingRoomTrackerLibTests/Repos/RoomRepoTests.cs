using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingRoomTrackerLib.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingRoomTrackerLib;
using Microsoft.EntityFrameworkCore;

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


        [TestMethod()]
        public void AddRoomTest()
        {
            var roomToBeAdded = new Room()
            {
                Name = "test",
                RoomType = RoomTypeEnum.Mødelokale,
                Building = BuildingEnum.A,
                Floor = 2
            };
            var addedRoom = _repo.Add(roomToBeAdded);
            Assert.IsNotNull(addedRoom);
            Assert.AreEqual(1, addedRoom.Id);
            Assert.AreEqual(roomToBeAdded.Name, addedRoom.Name);

        }

        [TestMethod()]
        public void GetAllRoomTest()
        {
            var rooms = _repo.GetAll();
            Assert.IsNotNull(rooms);
            Assert.AreEqual(0, rooms.Count());
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.Fail();
        }
    }
}