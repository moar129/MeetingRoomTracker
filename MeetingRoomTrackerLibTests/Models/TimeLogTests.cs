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
            Assert.AreEqual(startEvent, timelog.StartEvent);
            Assert.AreEqual(endEvent, timelog.EndEvent);
        }

        [TestMethod()]
        public void TimeLog_StartEventValidationTest()
        {
            var timelog = new TimeLog();


            // årstal < 2024
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                timelog.StartEvent = new DateTime(2023, 12, 31));


            // Sæt endEvent først (ellers giver testen ingen mening)
            timelog.EndEvent = new DateTime(2025, 11, 25, 15, 0, 0);


            // start > end
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                timelog.StartEvent = new DateTime(2025, 11, 25, 16, 0, 0));
        }

        [TestMethod()]
        public void TimeLog_EndEventValidationTest()
        {
            var startEvent = new DateTime(2025, 11, 25, 14, 0, 0);
            var timelog = new TimeLog();
            timelog.StartEvent = startEvent;


            // årstal < 2024
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                timelog.EndEvent = new DateTime(2023, 12, 31));


            // end <= start
            Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                timelog.EndEvent = new DateTime(2025, 11, 25, 13, 0, 0));
            timelog.StartEvent = new DateTime(2025, 11, 25, 14, 0, 0);


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