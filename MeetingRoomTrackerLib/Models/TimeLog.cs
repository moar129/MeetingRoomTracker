
namespace MeetingRoomTrackerLib.Models
{
    //TODO: Tilføj validering for TimeLog-klassen, Så som at startEvent skal være før endEvent osv.
    public class TimeLog
    {
        // Overvej om vi skal bruge IntervalTimer, eller om det er overflødigt information.
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartEvent { get; set; }
        public DateTime EndEvent { get; set; }

        public DateTime IntervalTimer { get; set; }

        public virtual Room Room { get; set; } = null;

        public TimeLog(int id, int roomId, DateTime startEvent, DateTime endEvent, DateTime intervalt)
        {
            Id = id;
            RoomId = roomId;
            StartEvent = startEvent;
            EndEvent = endEvent;
            IntervalTimer = intervalt;
        }
        public TimeLog()
        {}

        public override string ToString()
        {
            return $"Id: {Id}, RoomId: {RoomId}, StartEvent: {StartEvent:dd.MM.yyyy HH:mm:ss}, EndEvent: {EndEvent:dd.MM.yyyy HH:mm:ss}, IntervalTimer: {IntervalTimer:dd.MM.yyyy HH:mm:ss}";
        }

    }
}
