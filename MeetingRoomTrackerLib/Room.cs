namespace MeetingRoomTrackerLib
{
    public class Room
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public RumTypeEnum RumType { get; set; }
        public string Name { get; set; }
        public BygningEnum Bygning { get; set; }
        public int Floor { get; set; }

        public Room(int id, bool status, RumTypeEnum rumType, string name, BygningEnum bygning, int floor)
        {
            Id = id;
            Status = status;
            RumType = rumType;
            Name = name;
            Bygning = bygning;
            Floor = floor;
        }
        public Room()
        {
        }



        public override string ToString()
        {
            return $"Room ID: {Id}, Name: {Name}, Type: {RumType}, Building: {Bygning}, Floor: {Floor}, Status: {(Status ? "Occupied" : "Available")}";
        }
    }
}
