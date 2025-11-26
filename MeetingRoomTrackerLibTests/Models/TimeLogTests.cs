using MeetingRoomTrackerLib.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomTrackerLibTests.Models
{
    [TestClass()]
    public class TimeLogTests
    {
        [TestMethod()]
        public void TimeLogDefualtConstructorTest()
        {
            var timelog = new TimeLog();
            Assert.IsNotNull(timelog);


        }


        [TestMethod()]
        public void TimeLog_ParameterizedConstructorTest()
        {
            var startEvent = new DateTime(2025, 11, 25, 14, 0, 0);
            var endEvent = new DateTime(2025, 11, 25, 15, 30, 0);
            var timelog = new TimeLog(0, 5, startEvent, endEvent);
            Assert.AreEqual(0, timelog.Id);
            Assert.AreEqual(5, timelog.RoomId);
            Assert.AreEqual(startEvent, timelog.StartEvent);
            Assert.AreEqual(endEvent, timelog.EndEvent);
        }





        [TestMethod()]
        public void ToStringTest()
        {
            var TimeLog = new TimeLog(0,5,new DateTime(2025, 11, 25, 14, 0, 0), new DateTime(2025, 11, 25, 15, 30, 0)
        );
            Assert.AreEqual(TimeLog.ToString(), "Id: 0, RoomId: 5, StartEvent: 25.11.2025 14:00:00, EndEvent: 25.11.2025 15:30:00");
            Assert.IsNotNull(TimeLog.ToString());
        }
    }
}