using MeetingRoomTrackerLib.Repos;
using Microsoft.AspNetCore.Mvc;
using MeetingRoomTrackerLib;
using MeetingRoomTrackerLib.Models;
using MeetingRoomTrackerApi.DTOs;
using MeetingRoomTrackerLib.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetingRoomTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private ITimeLogService _timeLogService;

        public TimeLogController(ITimeLogService timeLogService)
        {
            _timeLogService = timeLogService;
        }


        // GET: api/<ValuesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult <IEnumerable<TimeLog>> GetAll()
        {
            List<TimeLog> timeLogs = _timeLogService.GetAllTimeLogs().ToList();
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
            try
            {
                TimeLog timeLog = _timeLogService.GetTimeLogById(id);
                return Ok(timeLog);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message.ToString());
            }
        }

        // POST api/<ValuesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<TimeLog> Post([FromBody] TimeLogDTO newTimeLog)
        {
            // VALIDATION FIRST — outside the try
            if (newTimeLog.StartEvent is null)
                return BadRequest("StartEvent is required.");

            if (newTimeLog.RoomId is null)
                return BadRequest("RoomId is required.");

            try
            {
                TimeLog timeLogToAdd = new TimeLog
                {
                    StartEvent = newTimeLog.StartEvent.Value,
                    EndEvent = newTimeLog.EndEvent,
                    RoomId = newTimeLog.RoomId.Value
                };

                _timeLogService.CreateTimeLog(timeLogToAdd);
                return CreatedAtAction(nameof(Get), new { id = timeLogToAdd.Id }, timeLogToAdd);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ValuesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public ActionResult<TimeLog> Put(int id, [FromBody] TimeLogDTO timeLog)
        {
            // VALIDATION FIRST — outside try
            if (timeLog.StartEvent is null)
                return BadRequest("StartEvent is required.");

            if (timeLog.RoomId is null)
                return BadRequest("RoomId is required.");

            try
            {
                TimeLog timelogToUpdate = new TimeLog
                {
                    Id = id,
                    StartEvent = timeLog.StartEvent.Value,
                    EndEvent = timeLog.EndEvent,
                    RoomId = timeLog.RoomId.Value
                };

                _timeLogService.UpdateTimeLog(timelogToUpdate);
                return Ok(timelogToUpdate);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                return Ok(_timeLogService.DeleteTimeLog(id));
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
