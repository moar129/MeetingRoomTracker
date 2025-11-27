using Microsoft.VisualStudio.TestTools.UnitTesting;
using MeetingRoomTrackerLib.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MeetingRoomTrackerLib.Models;

namespace MeetingRoomTrackerLib.Repos.Tests
{
    [TestClass()]
    public class TimeLogRepoTests
    {
        private static IRepos<TimeLog> _repo;
        [TestInitialize]
        public void TestInitialize()
        {
            // (UseInMemoryDatabase) In-memory database setup for testing purposes. This tells EF Core to use its InMemory provider instead of a real SQL database.
            // (databaseName: Guid.NewGuid().ToString()) This generates a unique database name every time using a new GUID.
            // after use of the test, the in-memory database is discarded, ensuring that each test runs in isolation without any leftover data from previous tests.

            var options = new DbContextOptionsBuilder<RMTDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // fresh for every test
                .Options;

            var context = new RMTDbContext(options);

            _repo = new TimeLogRepo(context);

        }
        private TimeLog CreateLog(
            int roomId = 1,
            DateTime startEvent = default,
            DateTime endEvent = default)
        {
            return new TimeLog
            {
                RoomId = roomId,
                StartEvent = startEvent == default ? DateTime.Now : startEvent,
                EndEvent = endEvent == default ? DateTime.Now.AddHours(1) : endEvent,
            };
        }


        [TestMethod()]
        public void AddLogTest()
        {
            var log = CreateLog();
            _repo.Add(log);
            Assert.AreEqual(1, _repo.GetAll().Count());

        }

        [TestMethod()]
        public void GetAllLogsTest()
        {
            var log1 = CreateLog();
            var log2 = CreateLog();
            _repo.Add(log1);
            _repo.Add(log2);
            var logs = _repo.GetAll();
            Assert.IsNotNull(logs);
            Assert.AreEqual(2, logs.Count());
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            var log = CreateLog(3);
            _repo.Add(log);
            var retrievedLog = _repo.GetById(1);
            Assert.IsNotNull(retrievedLog);
            Assert.AreEqual(3, retrievedLog.RoomId);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var logToBeUpådated = CreateLog();
            _repo.Add(logToBeUpådated);
            logToBeUpådated.RoomId = 2;
            _repo.Update(logToBeUpådated, 1);
            var updatedLog = _repo.GetById(1);
            Assert.AreEqual(2, updatedLog.RoomId);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            var logToBeDeleted = CreateLog();
            _repo.Add(logToBeDeleted);
            _repo.Delete(1);
            Assert.AreEqual(0, _repo.GetAll().Count());
        }
    }
}