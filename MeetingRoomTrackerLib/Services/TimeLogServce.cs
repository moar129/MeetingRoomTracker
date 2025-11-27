using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingRoomTrackerLib.Models;
using MeetingRoomTrackerLib.Repos;

namespace MeetingRoomTrackerLib.Services
{
    public class TimeLogServce : ITimeLogService
    {
        private readonly IRepos<TimeLog> _timeLogRepo;
        public TimeLogServce(IRepos<TimeLog> timeLogs) { _timeLogRepo = timeLogs; }

        public IEnumerable<TimeLog> GetAllTimeLogs()
        {
            return _timeLogRepo.GetAll();
        }

        public TimeLog GetTimeLogById(int id)
        {
            TimeLog? timeLog = _timeLogRepo.GetById(id);
            if (timeLog == null)
            {
                throw new KeyNotFoundException($"TimeLog with ID {id} not found.");
            }
            return timeLog;
        }

        public TimeLog CreateTimeLog(TimeLog timeLog)
        {
            if (timeLog == null)
            {
                throw new ArgumentNullException(nameof(timeLog), "TimeLog cannot be null.");
            }
            return _timeLogRepo.Add(timeLog);
        }

        public TimeLog UpdateTimeLog(TimeLog timeLog)
        {
            if (timeLog == null)
            {
                throw new ArgumentNullException(nameof(timeLog), "TimeLog cannot be null.");
            }
            TimeLog? existingTimeLog = _timeLogRepo.GetById(timeLog.Id);
            if (existingTimeLog == null)
            {
                throw new KeyNotFoundException($"TimeLog with ID {timeLog.Id} not found.");
            }
            return _timeLogRepo.Update(timeLog, timeLog.Id);
        }

        public TimeLog DeleteTimeLog(int id)
        {
            TimeLog? existingTimeLog = _timeLogRepo.GetById(id);
            if (existingTimeLog == null)
            {
                throw new KeyNotFoundException($"TimeLog with ID {id} not found.");
            }
            _timeLogRepo.Delete(id);
            return existingTimeLog;
        }
    }
}
