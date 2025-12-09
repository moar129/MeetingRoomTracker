using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingRoomTrackerLib.Services.Discord_WebHook
{
    // Interface for Discord WebHook Service 
    public interface IDiscordWebHookService
    {
        Task SendMessageAsync(string message);
        Task SendEmbedMessageAsync(string title, string description, int color = 1127128);

    }
}
