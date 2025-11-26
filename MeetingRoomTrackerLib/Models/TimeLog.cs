
namespace MeetingRoomTrackerLib.Models
{
    public class TimeLog
    {
        private DateTime _startEvent;
        private DateTime _endEvent;
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartEvent 
        { 
            get { return _startEvent; }
            set
            {
                if (value > EndEvent)
                {
                    throw new ArgumentException("StartEvent must be earlier than EndEvent.");
                }
                if (value.Year > 2024)
                {
                    throw new ArgumentException("StartEvent year cannot be greater than 2024.");
                }

                _startEvent = value;
            } 
        }
        public DateTime EndEvent 
        {
            get { return _endEvent; }
            set
            {
                if (value < StartEvent)
                {
                    throw new ArgumentException("EndEvent must be later than StartEvent.");
                }
                if (value.Year > 2024)
                {
                    throw new ArgumentException("EndEvent year cannot be greater than 2024.");
                }
                _endEvent = value;
            } 
        }
        public virtual Room Room { get; set; } = null;

        public TimeLog(int id, int roomId, DateTime startEvent, DateTime endEvent, DateTime intervalt)
        {
            Id = id;
            RoomId = roomId;
            StartEvent = startEvent;
            EndEvent = endEvent;
        }
        public TimeLog()
        {}

        public override string ToString()
        {
            return $"Id: {Id}, RoomId: {RoomId}, StartEvent: {StartEvent:dd.MM.yyyy HH:mm:ss}, EndEvent: {EndEvent:dd.MM.yyyy HH:mm:ss}";
        }

    }
}
