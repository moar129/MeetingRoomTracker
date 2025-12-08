using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetingRoomTrackerLib.Models;
using MeetingRoomTrackerLib.Repos;
using MeetingRoomTrackerLib.Services.Discord_WebHook;

namespace MeetingRoomTrackerLib.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRepos<Room> _roomRepo;
        private readonly IDiscordWebHookService _discordWebHookService;

        public RoomService(IRepos<Room> rooms, IDiscordWebHookService discordService)
        {
            _roomRepo = rooms;
            _discordWebHookService = discordService; 
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomRepo.GetAll();
        }
        public Room GetRoomById(int id)
        {
            Room? room = _roomRepo.GetById(id);
            if (room == null)
            {
                throw new KeyNotFoundException($"Room with ID {id} not found.");
            }
            return room;
        }
        public Room CreateRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            }
            return _roomRepo.Add(room);
        }
        public async Task <Room> UpdateRoom(Room room)
        {
            if (room == null)
            {
                throw new ArgumentNullException(nameof(room), "Room cannot be null.");
            }
            Room? existingRoom = _roomRepo.GetById(room.Id);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException($"Room with ID {room.Id} not found.");
            }

            bool oldStatus = existingRoom.Status;

            Room updatedRoom = _roomRepo.Update(room, room.Id);

            if (oldStatus == true && updatedRoom.Status == false)
            {
               await SendRoomFreeNotification(updatedRoom);

            }
            return updatedRoom;
        }
        /// <summary>
        /// Sends a notification to Discord when a room becomes free.
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        private async Task SendRoomFreeNotification(Room room)
        {
            string roomName = room.Name ?? "Ukendt Lokale";
            
            await _discordWebHookService.SendEmbedMessageAsync(
                title: $"Lokale Ledigt: {roomName}",
                description: $"Lokalet **{roomName}** er nu ledigt.",
                color: 65280 // Grøn farve
            );

        }

        public Room DeleteRoom(int id)
        {
            Room? existingRoom = _roomRepo.GetById(id);
            if (existingRoom == null)
            {
                throw new KeyNotFoundException($"Room with ID {id} not found.");
            }
            _roomRepo.Delete(id);
            return existingRoom;
        }
        public bool GetStatus(int id)
        {
            Room? room = _roomRepo.GetById(id);
            if (room == null)
            {
                throw new KeyNotFoundException($"Room with ID {id} not found.");
            }
            return room.Status;
        }

    }
}
