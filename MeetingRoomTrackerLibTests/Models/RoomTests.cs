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
        public void defaultConstructorTest()
        {
            
            var room = new Room();
            Assert.IsNotNull(room);

            
        }

        [TestMethod()]
        public void construktorTest()
        {
            var roomToBeAdded = new Room()
            {
                Name = "test",
                Status = true,
                RoomType = RoomTypeEnum.Mødelokale,
                Building = BuildingEnum.A,
                Floor = 2
            };

            Assert.AreEqual(roomToBeAdded.Name, "test");
            Assert.AreEqual(roomToBeAdded.RoomType, RoomTypeEnum.Mødelokale);
            Assert.AreEqual(roomToBeAdded.Building, BuildingEnum.A);
            Assert.AreEqual(roomToBeAdded.Floor, 2);

        }
        [TestMethod()]
        public void nameTest()
        {
            var room = new Room();
            Console.WriteLine(room.ToString());
            Assert.ThrowsException<ArgumentNullException>(() => room.Name = "");
            Assert.ThrowsException<ArgumentNullException>(() => room.Name = "   ");
            Assert.ThrowsException<ArgumentNullException>(() => room.Name = null);
        }
        [TestMethod()]
        public void floorTest()
        {
            var room = new Room();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => room.Floor = -1);
        }
    }
}