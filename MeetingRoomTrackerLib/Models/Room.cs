
namespace MeetingRoomTrackerLib
{
    // TODO: Tilføj validering for Room-klassen, Så som en string må ikke være tom, Floor skal være positiv osv.
    public class Room
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public RoomTypeEnum RoomType { get; set; }
        public string Name { get; set; }
        public BuildingEnum Building { get; set; }
        public int Floor { get; set; }

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
