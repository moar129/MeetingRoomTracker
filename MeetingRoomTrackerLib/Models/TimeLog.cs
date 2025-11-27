
namespace MeetingRoomTrackerLib.Models
{
    /// <summary>
    /// TimeLog class represents a log entry for a meeting room sensor with start and end times.
    /// </summary>
    public class TimeLog
    {
        private DateTime _startEvent;
        private DateTime _endEvent;
        public int Id { get; set; }
        public int RoomId { get; set; }
        /// <summary>
        /// Start time of the event with validation to ensure it is before EndEvent and year is 2024 or later.
        /// </summary>
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
        /// <summary>
        /// End time of the event with validation to ensure it is after StartEvent and year is 2024 or later.
        /// </summary>
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
        /// <summary>
        /// Reference to the associated Room object. For EF relationships.
        /// </summary>
        public virtual Room Room { get; set; } = null;

        /// <summary>
        /// TimeLog constructor with parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="roomId"></param>
        /// <param name="startEvent"></param>
        /// <param name="endEvent"></param>
        public TimeLog(int id, int roomId, DateTime startEvent, DateTime endEvent)
        {
            Id = id;
            RoomId = roomId;
            StartEvent = startEvent;
            EndEvent = endEvent;
        }
        /// <summary>
        /// TimeLog default constructor mostly for EF
        /// </summary>
        public TimeLog()
        {}

        public override string ToString()
        {
            return $"Id: {Id}, RoomId: {RoomId}, StartEvent: {StartEvent:dd.MM.yyyy HH:mm:ss}, EndEvent: {EndEvent:dd.MM.yyyy HH:mm:ss}";
        }

    }
}
