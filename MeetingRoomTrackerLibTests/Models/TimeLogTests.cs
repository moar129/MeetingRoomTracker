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
        public void TimeLogConstructorTest()
        {
            var timelog = new TimeLog();
            Assert.IsNotNull(timelog);


        }

        [TestMethod()]
        public void ToStringTest()
        {
            var TimeLog = new TimeLog(
            id: 0,                                   // EF Core sætter selv Id når du gemmer
            roomId: 5,                                 // f.eks. mødelokale nr. 5
            startEvent: new DateTime(2025, 11, 25, 14, 0, 0),  // 25. november 2025 kl. 14:00
            endEvent: new DateTime(2025, 11, 25, 15, 30, 0), // slutter kl. 15:30
            intervalt: new DateTime(2025, 11, 25, 13, 55, 0)  // intervallet startede 5 minutter før mødet
        );
            Assert.AreEqual(TimeLog.ToString(), "Id: 0, RoomId: 5, StartEvent: 25.11.2025 14:00:00, EndEvent: 25.11.2025 15:30:00, IntervalTimer: 25.11.2025 13:55:00"
                );
            Assert.IsNotNull(TimeLog.ToString());
        }
    }
}