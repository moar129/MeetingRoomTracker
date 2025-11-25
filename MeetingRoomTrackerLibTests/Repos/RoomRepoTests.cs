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
            optionsBuilder.UseSqlServer();
            var context = new RMTDbContext(optionsBuilder.Options);
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Rooms");
            _repo = new RoomRepo(context);
        }
        [TestMethod()]
        public void RoomRepoTest()
        {
            Assert.Fail();
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

        [TestMethod()]
        public void CreateTest()
        {
            Assert.Fail();
        }
    }
}