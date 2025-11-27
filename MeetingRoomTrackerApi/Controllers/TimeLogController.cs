using MeetingRoomTrackerLib.Repos;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomTrackerLib;
using MeetingRoomTrackerLib.Models;
using MeetingRoomTrackerApi.DTOs;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetingRoomTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private IRepos<TimeLog> _timeLogRepo;

        public TimeLogController(IRepos<TimeLog> repo)
        {
            _timeLogRepo = repo;
        }


        // GET: api/<ValuesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult <IEnumerable<TimeLog>> GetAll()
        {
            List<TimeLog> timeLogs = new List<TimeLog>(_timeLogRepo.GetAll());
            if (timeLogs.Count == 0)
            {
                return NotFound("No time logs found.");
            }
            return Ok(timeLogs);
        }

        // GET api/<ValuesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<TimeLog> Get([FromRoute]int id)
        {
            TimeLog? timeLog = _timeLogRepo.GetById(id);
            if (timeLog == null)
            {
                return NotFound($"TimeLog with ID {id} not found.");
            }
            return Ok(timeLog);
        }

        // POST api/<ValuesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost]
        public ActionResult<TimeLog> Post([FromBody] TimeLogDTO newTimeLog)
        {
            try
            {
                TimeLog timeLogToAdd = new TimeLog
                {
                    StartEvent = newTimeLog.StartEvent!.Value,
                    EndEvent = newTimeLog.EndEvent!.Value,
                    RoomId = newTimeLog.RoomId!.Value
                };
                _timeLogRepo.Add(timeLogToAdd);
                return CreatedAtAction(nameof(Get), new { id = timeLogToAdd.Id }, timeLogToAdd);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public ActionResult<TimeLog> Put(int id, [FromBody]TimeLogDTO timeLog)
        {
            try
            {
                TimeLog timelogTOUpdate = new TimeLog
                {
                    Id = id,
                    StartEvent = timeLog.StartEvent!.Value,
                    EndEvent = timeLog.EndEvent!.Value,
                    RoomId = timeLog.RoomId!.Value
                };
                _timeLogRepo.Update(timelogTOUpdate,id);
                return Ok(timelogTOUpdate);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(_timeLogRepo.Delete(id));
        }
    }
}
