using MeetingRoomTrackerLib.EnumClasses;

namespace MeetingRoomTrackerLib
{
    /// <summary>
    /// Represents a meeting room with properties such as ID, status, type, name, building, and floor.
    /// </summary>
    public class Room
    {
        private string _name;
        private int _floor;
        private int _roomNumber;
        public int Id { get; set; }
        public bool Status { get; set; }
        public RoomTypeEnum RoomType { get; set; }
        /// <summary>
        /// Name of the room with validation to ensure it is not null or empty.
        /// </summary>
        public string Name 
        {
            get { return _name; }
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Name cannot be null or empty.");
                }
                _name = value;
            }
        }
        public BuildingEnum Building { get; set; }
        /// <summary>
        /// Floor number with validation to ensure it is non-negative.
        /// </summary>
        public int Floor 
        {
            get { return _floor; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Floor must be a non-negative integer.");
                }
                _floor = value;
            } 
        }
        public int RoomNumber 
        { 
            get { return _roomNumber; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("RoomNumber must be a non-negative integer.");
                }
                _roomNumber = value;
            } 
        }
        /// <summary>
        /// creates a room with parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="rumType"></param>
        /// <param name="name"></param>
        /// <param name="bygning"></param>
        /// <param name="floor"></param>
        /// <param name="roomNumber"></param>
        public Room(int id, bool status, RoomTypeEnum rumType, string name, BuildingEnum bygning, int floor, int roomNumber)
        {
            Id = id;
            Status = status;
            RoomType = rumType;
            Name = name;
            Building = bygning;
            Floor = floor;
            RoomNumber = roomNumber;
        }
        /// <summary>
        /// Creates a room without parameters mostly used by Entity Framework
        /// </summary>
        public Room()
        {
        }



        public override string ToString()
        {
            return $"Room ID: {Id}, Name: {Name}, Type: {RoomType}, Building: {Building}, Floor: {Floor}, Status: {(Status ? "Occupied" : "Available")}";
        }
    }
}
