import RPi.GPIO as GPIO
import time
import json
import os
from datetime import datetime, timedelta

# Manuel ændring af Room
RoomId = 12     # <- Ændr dette til det rum sensoren sidder i

PIR_PIN = 11
EVENT_TIMEOUT_MINUTES = 0.3      # <- ændr dette til hvor mange minutter timeren skal være
LOG_FILE = "event_log.json"

GPIO.setmode(GPIO.BCM)
GPIO.setup(PIR_PIN, GPIO.IN)

# Lav logfil hvis den ikke findes
if not os.path.exists(LOG_FILE):
    with open(LOG_FILE, "w") as f:
        json.dump([], f)

event_active = False
event_end_time = None  # tidspunkt hvor event slutter hvis ingen bevægelse

def log_event(event_type):
    data = {
        "event": event_type,
        "roomId": RoomId,
        "timestamp": datetime.now().isoformat()
    }
    with open(LOG_FILE, "r") as f:
        log = json.load(f)
    log.append(data)
    with open(LOG_FILE, "w") as f:
        json.dump(log, f, indent=4)
    print("Logged:", data)

print("PIR-sensor aktiv. Tryk Ctrl+C for at stoppe.")

try:
    while True:
        motion = GPIO.input(PIR_PIN)

        if motion:
            print("Bevægelse registreret")

            # Hvis der IKKE allerede er et event aktiv → start et
            if not event_active:
                print("EventStart")
                log_event("EventStart")
                event_active = True

            # Nulstil timeren (forlænger eventet)
            event_end_time = datetime.now() + timedelta(minutes=EVENT_TIMEOUT_MINUTES)

        else:
            print("Ingen bevægelse")

            # Hvis et event er aktivt OG tiden er gået → stop eventet
            if event_active and datetime.now() > event_end_time:
                print("EventStop")
                log_event("EventStop")
                event_active = False

        time.sleep(1)

except KeyboardInterrupt:
    print("Afslutter...")
    GPIO.cleanup()
