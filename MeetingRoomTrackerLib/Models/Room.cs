
using MeetingRoomTrackerLib.EnumClasses;

namespace MeetingRoomTrackerLib
{
    public class Room
    {
        private string _name;
        private int _floor;
        public int Id { get; set; }
        public bool Status { get; set; }
        public RoomTypeEnum RoomType { get; set; }
        public string Name 
        {
            get { return _name; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be null or empty.");
                }
                _name = value;
            }
        }
        public BuildingEnum Building { get; set; }
        public int Floor 
        {
            get { return _floor; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Floor must be a non-negative integer.");
                }
                _floor = value;
            } 
        }

        public Room(int id, bool status, RoomTypeEnum rumType, string name, BuildingEnum bygning, int floor)
        {
            Id = id;
            Status = status;
            RoomType = rumType;
            Name = name;
            Building = bygning;
            Floor = floor;
        }
        public Room()
        {
        }



        public override string ToString()
        {
            return $"Room ID: {Id}, Name: {Name}, Type: {RoomType}, Building: {Building}, Floor: {Floor}, Status: {(Status ? "Occupied" : "Available")}";
        }
    }
}
