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
                Floor = 2,
                RoomNumber = 101
            };

            Assert.AreEqual(roomToBeAdded.Name, "test");
            Assert.AreEqual(roomToBeAdded.RoomType, RoomTypeEnum.Mødelokale);
            Assert.AreEqual(roomToBeAdded.Building, BuildingEnum.A);
            Assert.AreEqual(roomToBeAdded.Floor, 2);
            Assert.AreEqual(roomToBeAdded.RoomNumber, 101);

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
        [TestMethod()]
        public void roomNumberTest()
        {
            var room = new Room();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => room.RoomNumber = -1);
        }
        [TestMethod()]
        public void ToStringTest()
        {
            var room = new Room(1, true, RoomTypeEnum.Mødelokale, "TestRoom", BuildingEnum.B, 3,5);
            var expectedString = "Room ID: 1, Name: TestRoom, Type: Mødelokale, Building: B, Floor: 3, Status: Occupied";
            Assert.AreEqual(expectedString, room.ToString());
            Assert.IsNotNull(room.ToString());
        }
    }
}