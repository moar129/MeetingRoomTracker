using MeetingRoomTrackerLib.EnumClasses;

namespace MeetingRoomTrackerApi.DTOs
{
    public class RoomDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public RoomTypeEnum? RoomType { get; set; }
        public BuildingEnum? Building { get; set; }
        public int? Floor { get; set; }
        public RoomDTO() {}
    }
}
