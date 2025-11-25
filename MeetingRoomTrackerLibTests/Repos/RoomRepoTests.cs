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


             _repo.Add(roomToBeAdded);
            
            
            Assert.AreEqual(1, _repo.GetAll().Count());
            Assert.AreEqual(roomToBeAdded.Name, _repo.GetById(1).Name);

        }

        [TestMethod()]
        public void GetAllRoomTest()
        {
            var roomToBeAdded = new Room()
            {
                Name = "test",
                RoomType = RoomTypeEnum.Mødelokale,
                Building = BuildingEnum.A,
                Floor = 2
            };
            var roomToBeAdded2 = new Room()
            {
                Name = "test2",
                RoomType = RoomTypeEnum.Mødelokale,
                Building = BuildingEnum.A,
                Floor = 2
            };

            _repo.Add(roomToBeAdded);
            _repo.Add(roomToBeAdded2);

            var rooms = _repo.GetAll();
            Assert.IsNotNull(rooms);
            Assert.AreEqual(2, rooms.Count());

           
           
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var roomToBeAdded = new Room()
            {
                Name = "test",
                RoomType = RoomTypeEnum.Mødelokale,
                Building = BuildingEnum.A,
                Floor = 2
            };
            _repo.Add(roomToBeAdded);
            var room = _repo.GetById(1);
            Assert.AreEqual(roomToBeAdded.Name, room.Name);
            
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var roomToBeUpdated = new Room()
            {
                Name = "test",
                RoomType = RoomTypeEnum.Mødelokale,
                Building = BuildingEnum.A,
                Floor = 2
            };
            _repo.Add(roomToBeUpdated);
            roomToBeUpdated.Name = "test2";
            _repo.Update(roomToBeUpdated, 1);
            var room = _repo.GetById(1);
            Assert.AreEqual(roomToBeUpdated.Name, room.Name);
            
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var roomToBeDelete = new Room()
            {
                Name = "test",
                RoomType = RoomTypeEnum.Mødelokale,
                Building = BuildingEnum.A,
                Floor = 2
            };
            _repo.Add(roomToBeDelete);
            _repo.Delete(1);
            Assert.AreEqual(0, _repo.GetAll().Count());
            
        }
    }
}