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
            var optionsBuilder = new DbContextOptionsBuilder<RMTDbContext>();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RMTDb;Integrated Security=True;Connect Timeout=30;Encrypt=False");
            var context = new RMTDbContext(optionsBuilder.Options);
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Rooms");
            _repo = new RoomRepo(context);
        }

        [TestMethod()]
        public void AddTest()
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
        public void GetAllTest()
        {
            Assert.Fail();
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