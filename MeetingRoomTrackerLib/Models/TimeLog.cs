using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomTrackerLib.Models
{
     public class TimeLog
    {
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
            return $"Id: {Id}, RoomId: {RoomId}, StartEvent: {StartEvent}, EndEvent: {EndEvent}, Bookig: {IntervalTimer}";
        }

    }
}
