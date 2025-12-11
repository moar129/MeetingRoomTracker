# Meeting Room Tracker

## Oversigt
Meeting Room Tracker er et realtime-system, der giver et præcist overblik over tilgængeligheden af grupperum, mødelokaler og klasselokaler på Zealand. Systemet anvender bevægelsessensorer og Raspberry Pi-enheder til at registrere aktivitet i lokaler og formidler denne information til brugerne gennem en webbaseret grænseflade.

## Formål
Projektet løser behovet for et hurtigt, pålideligt og automatiseret overblik over, hvilke lokaler der er ledige eller optagede—uden at brugerne skal afsøge campus fysisk.

## Funktioner
- Registrering af aktivitet via PIR-sensorer  
- Realtime-status for alle tilgængelige lokaler  
- Enkelt og overskueligt frontend-interface  
- Kommunikation via UDP mellem Raspberry Pi og backend  

## Teknologistak
- **Backend:** .NET / C#  
- **Frontend:** Vue.js 
- **Hardware:** Raspberry Pi + PIR-sensor  
- **Kommunikation:** UDP-broadcasts  
- **Tests:** Unit tests,Selenium og Postman/swagger dokumentation

## Projektstruktur
### `MeetingRoomTracker RaspberryPi/`
Sensor- og eventhåndtering, inkl. UDP-broadcasts.

### `MeetingRoomTrackerApi/`
REST API til modtagelse og behandling af sensordata.

### `MeetingRoomTrackerFrontEnd/`
Vue.js-baset UI, der viser rumstatus i realtime.

### `MeetingRoomTrackerLib/`
Fælles modeller, services og domænelogik.

### `MeetingRoomTrackerLibTests/`
Unit tests og automatiserede testscenarier.

### `UDPMeetingRoomTracker/`
Håndtering af UDP-kommunikation mellem hardware og backend.
