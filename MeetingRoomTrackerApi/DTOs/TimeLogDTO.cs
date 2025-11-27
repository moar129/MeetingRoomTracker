namespace MeetingRoomTrackerApi.DTOs
{
    public class TimeLogDTO
    {
        public int? Id { get; set; }
        public int? RoomId { get; set; }
        public DateTime? StartEvent { get; set; }
        public DateTime? EndEvent { get; set; }
        public TimeLogDTO() { }
    }
}
