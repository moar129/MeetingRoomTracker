using MeetingRoomTrackerLib;
using MeetingRoomTrackerLib.EnumClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MeetingRoomTrackerLibTests.Models
{
    [TestClass()]
    public class RoomTests
    {
        [TestMethod()]
        public void Test_construktor()
        {
            
            var room = new Room();
            Assert.IsNotNull(room);

            
        }

        [TestMethod()]
        public void Test_the_maine_construktor()
        {
            var roomToBeAdded = new Room()
            {
                Name = "test",
                RoomType = RoomTypeEnum.Mødelokale,
                Building = BuildingEnum.A,
                Floor = 2
            };

            Assert.AreEqual(roomToBeAdded.Name, "test");
            Assert.AreEqual(roomToBeAdded.RoomType, RoomTypeEnum.Mødelokale);
            Assert.AreEqual(roomToBeAdded.Building, BuildingEnum.A);
            Assert.AreEqual(roomToBeAdded.Floor, 2);

        }

        
    }
}