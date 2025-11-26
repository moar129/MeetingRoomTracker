
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
            get => _startEvent; 
            set
            {
                // vi smider en fjel hvis startEvent er større end endEvent
                if ( value > _endEvent && _endEvent != default(DateTime))
                {
                    throw new ArgumentOutOfRangeException(nameof(StartEvent));
                }
                // vi smider en fejl hvis årstallet er mindre end 2024
                if (value.Year < 2024)
                {
                    throw new ArgumentOutOfRangeException(nameof(StartEvent));
                }

                

                _startEvent = value;
            } 
        }
        public DateTime EndEvent 
        {
            get => _endEvent; 
            set
            {
                // vi smider en fejl hvis endEvent er mindre end startEvent
                if ( value <= _startEvent && _startEvent != default(DateTime))
                {
                    throw new ArgumentOutOfRangeException(nameof(EndEvent));
                }
                // vi smider en fejl hvis årstallet er mindre end 2024
                if (value.Year < 2024)
                {
                    throw new ArgumentOutOfRangeException(nameof(EndEvent));
                }
                _endEvent = value;
            } 
        }
        public virtual Room Room { get; set; } = null;

        public TimeLog(int id, int roomId, DateTime startEvent, DateTime endEvent)
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
