# UDPMeetingRoomTracker/ServerUDP.py
# skal modtage UDP pakker fra Sensor og lave HTTP requests til Webserveren
from socket import *
import json
import requests

serverPort = 12000
serverSocket = socket(AF_INET, SOCK_DGRAM)

serverSocket.bind(("",serverPort))
print("The server is ready to receive")

# Store active timelog IDs
activeTimeLogs = {}

def send_http_request(Event):
    # Her kan du tilføje kode til at sende HTTP requests til Webserveren baseret på Event data
    try:
        roomId = Event["roomId"]
        Room = requests.get(f"https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net//api/rooms/{roomId}")
        room = Room.json()
        if Event["event"] == "EventStart":
            # Opret ny timelog
            timeLog = {
                "roomId": roomId,
                "startEvent": Event["timestamp"],
                "endEvent": None
            },
            
            # Opdater rummet til optaget
            room["status"] = True
            
            # Send POST request for timelog og PUT request for room
            responseTimeLog = requests.post(f"https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net//api/Timelog", json=timeLog)
            responseRoom = requests.put(f"https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net//api/rooms/{roomId}", json=room)
            
            # print responses for debugging
            print("HTTP ResponseTimeLog:", responseTimeLog.status_code, responseTimeLog.text)
            print("HTTP ResponseRoom:", responseRoom.status_code, responseRoom.text)

            # gem den nye timelog ID
            newId = responseTimeLog.json().get("id")
            activeTimeLogs[roomId] = newId

        elif Event["event"] == "EventStop":
            # Hent den aktive timelog ID for rummet
            if roomId not in activeTimeLogs:
                print(f"Error: No active timelog for room {roomId}")
                return
            timeLogId = activeTimeLogs[roomId]

            # Opdater timelog med sluttidspunkt og opdater rummet til ledig
            timeLog = requests.get(f"https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net//api/Timelog/{timeLogId}").json()
            timeLog["endEvent"] = Event["timestamp"]
            room["status"] = False

            # Send PUT requests for timelog og room
            responseTimeLog = requests.put(f"https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net//api/Timelog/{timeLogId}", json=timeLog)
            responseRoom = requests.put(f"https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net//api/rooms/{roomId}", json=room)
            
            # Fjern den gemte timelog ID
            del active_timelogs[roomId]

            # print responses for debugging
            print("HTTP ResponseTimeLog:", responseTimeLog.status_code, responseTimeLog.text)
            print("HTTP ResponseRoom:", responseRoom.status_code, responseRoom.text)
    except Exception as e:
        print("Error sending HTTP request:", e)

while True:
    message, clientAddress = serverSocket.recvfrom(2048)
    Event = json.loads(message.decode())
    print("Parsed:", Event)
    validResponse = True
    if Event["event"] not in ["EventStart", "EventStop"]:
        print("Invalid event type:", Event["event"])
        validResponse = False
    if validResponse:
        send_http_request(Event)
    
