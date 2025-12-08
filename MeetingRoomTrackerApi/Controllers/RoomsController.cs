using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MeetingRoomTrackerLib;
using MeetingRoomTrackerLib.Repos;
using MeetingRoomTrackerApi.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using MeetingRoomTrackerLib.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MeetingRoomTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        // GET: api/<RoomsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Room>> GetAll()
        {
            List<Room> rooms = _roomService.GetAllRooms().ToList();
            if (rooms.Count == 0)
            {
                return NotFound("No rooms found.");
            }
            return Ok(rooms);
        }

        // GET api/<RoomsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("{id}")]
        public ActionResult<Room> Get([FromRoute] int id)
            {
                try
                {
                    Room room = _roomService.GetRoomById(id);
                    return Ok(room);
                }
                catch (KeyNotFoundException ex)
                {
                    return NotFound(ex.Message.ToString());
                }
                catch (Exception ex)
                {
                    return StatusCode(500,ex.Message.ToString());
                }
        }


        // POST api/<RoomsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public ActionResult<Room> Post([FromBody] RoomDTO newRoom)
        {
            try
            {
                Room roomToAdd = new Room
                {
                    Name = newRoom.Name!,
                    RoomType = newRoom.RoomType!.Value,
                    Building = newRoom.Building!.Value,
                    Floor = newRoom.Floor!.Value,
                    Status = false, // Default status to false (available)
                    RoomNumber = newRoom.RoomNumber!.Value
                };
                Room createdRoom = _roomService.CreateRoom(roomToAdd);
                return CreatedAtAction(nameof(Get), new { id = createdRoom.Id }, createdRoom    );
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message.ToString());
            }


        }

        // PUT api/<RoomsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{id}")]

        // Opdaterer et lokale med angivet id async så det ikke blokerer hovedtråden
        public async Task<ActionResult<Room>> Put(int id, [FromBody] RoomDTO room)
        {
            // dette er en kommentar
            try
            {
                Room roomToUpdate = new Room
                {
                    Id = id,
                    Name = room.Name!,
                    RoomType = room.RoomType!.Value,
                    Building = room.Building!.Value,
                    Floor = room.Floor!.Value,
                    Status = room.Status!.Value,
                    RoomNumber = room.RoomNumber!.Value
                };
                // Opdaterer lokalet og venter på resultatet
                Room updatedRoom = await _roomService.UpdateRoom(roomToUpdate);
                return Ok(roomToUpdate);
                
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            catch (Exception ex)
            {
                return StatusCode(501, ex.Message.ToString());
            }
        }

        // DELETE api/<RoomsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Delete(int id)
        {
            return Ok(_roomService.DeleteRoom(id));

        }
        
       
    }
}
