using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Diagnostics;
using System.Linq.Expressions;

namespace MeetingRoomTrackerLib.Services.Discord_WebHook
{
    // Service til at sende beskeder til en Discord Webhook
    public class DiscordWebHookService : IDiscordWebHookService
    {
        // HttpClient bruges til at sende HTTP-anmodninger
        private readonly HttpClient _httpClient;
        private readonly string _webHookUrl;

        // Konstruktor, der initialiserer HttpClient og henter Webhook URL fra konfigurationen
        public DiscordWebHookService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _webHookUrl = configuration["Discord:WebhookUrl"] ?? throw new ArgumentException("Discord WebHook URL is not configured.");
        }

        // Sender en simpel tekstbesked til Discord Webhook async så det ikke blokerer hovedtråden
        public async Task SendMessageAsync(string message)
        {
            var payload = new { content = message };
            await SendPayloadAsync(payload);
        }

        // Sender en embed-besked til Discord Webhook async så det ikke blokerer hovedtråden
        public async Task SendEmbedMessageAsync(string title, string description, int color = 65280)
        {
            var embed = new
            {
                title = title,
                description = description,
                color = color,
                /// Tidspinkt til Discord standardformat.
                timestamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            };
            var payload = new { embeds = new[] { embed } };
            await SendPayloadAsync(payload);
        }

        // Hjælpefunktion til at sende payload til Discord Webhook
        public async Task SendPayloadAsync(object payload)
        {
            try
            {
                // Serialiserer payload til JSON
                var json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Sender POST-anmodningen til Discord Webhook URL
                var response = await _httpClient.PostAsync(_webHookUrl, content);

                // Vi tjekker, om statuskoden indikerer succes (f.eks. 200 OK eller 204 No Content)
                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    // Logger fejl fra Discord's API
                    Console.WriteLine($"[Discord Webhook FEJL] Status: {response.StatusCode}. Indhold: {errorContent}");
                }
            }
            catch (HttpRequestException ex)
            {
                // Håndterer netværksfejl (f.eks. DNS-fejl, timeout)
                Console.WriteLine($"[Discord Webhook KRITISK FEJL] Netværksfejl ved afsendelse: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Håndterer andre uventede fejl
                Console.WriteLine($"[Discord Webhook KRITISK FEJL] Uventet fejl: {ex.Message}");
            }
        }
    }
}
