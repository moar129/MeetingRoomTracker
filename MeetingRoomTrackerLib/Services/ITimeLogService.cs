using MeetingRoomTrackerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomTrackerLib.Services
{
    public interface ITimeLogService
    {
        IEnumerable<TimeLog> GetAllTimeLogs();
        TimeLog GetTimeLogById(int id);
        TimeLog CreateTimeLog(TimeLog log);
        TimeLog UpdateTimeLog(TimeLog log);
        TimeLog DeleteTimeLog(int id);
    }
}
