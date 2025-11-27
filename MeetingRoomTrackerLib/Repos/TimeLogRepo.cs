using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingRoomTrackerLib.Models;

namespace MeetingRoomTrackerLib.Repos
{
    public class TimeLogRepo: IRepos<TimeLog>
    {
        private RMTDbContext _context;
        public TimeLogRepo(RMTDbContext Context)
        {
            _context = Context;
        }

        public TimeLog Add(TimeLog timeLog)
        {
     
            _context.TimeLogs.Add(timeLog);
            _context.SaveChanges();
            return timeLog;
        }

        public IEnumerable<TimeLog> GetAll()
        {
            IEnumerable<TimeLog> timeLogs = _context.TimeLogs.ToList();
            return timeLogs;
        }

        public TimeLog? GetById(int id)
        {
            TimeLog? timeLog = _context.TimeLogs.FirstOrDefault(t => t.Id == id);
            return timeLog;
        } 
        
        public TimeLog? Update(TimeLog timeLogToBeUpdated, int id)
        {
            TimeLog? existingTimeLog = GetById(id);
            if (existingTimeLog != null)
            {
                existingTimeLog.RoomId = timeLogToBeUpdated.RoomId;
                existingTimeLog.StartEvent = timeLogToBeUpdated.StartEvent;
                existingTimeLog.EndEvent = timeLogToBeUpdated.EndEvent;
                _context.SaveChanges();
            }
            return existingTimeLog;
        }
        public TimeLog? Delete(int id) 
        {
            TimeLog? timeLogToBeDeleted = GetById(id);
            if (timeLogToBeDeleted != null)
            {
                _context.TimeLogs.Remove(timeLogToBeDeleted);
                _context.SaveChanges();
            }
            return timeLogToBeDeleted;
        }
    }
}
