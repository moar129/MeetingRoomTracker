import appTemplate from './App.html?raw'
import RoomView from './Room.js'

export default {
  template: appTemplate,
  name: 'App',
  components: {
    RoomView
  },
  data() {
    return {
      rooms: [],
      timeLogs: [],
      apiBaseUrl: import.meta.env.VITE_API_BASE_URL,
      apiURL: "https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net",
      statuses: {
        available: 'Ledig',
        occupied: 'Optaget',
        unknown: 'Mangler data'
      },
      selectedRoom: null,
      showRoomView: false,
    };
  },
  methods: {
    async fetchRooms() {
      try {
        const response = await fetch(`${this.apiBaseUrl}/api/rooms`);
        if (!response.ok) return;
        this.rooms = await response.json();
      } catch (err) {}
    },
    getStatusLabel(status) {
      return this.statuses[status] || this.statuses.unknown;
    },
    getStatusClass(status) {
      return status || 'unknown';
    },
    async openRoom(roomId) {
  try {
    // Load the room from the API
    const resRoom = await fetch(`${this.apiBaseUrl}/api/rooms/${roomId}`);
    if (!resRoom.ok) return;
    const room = await resRoom.json();

    // Load all timelogs
    const resLogs = await fetch(`${this.apiBaseUrl}/api/timelog`);
    let logs = [];
    if (resLogs.ok) logs = await resLogs.json();

    // Filter logs for this room
    const roomLogs = logs.filter(l => l.roomId === roomId);

    // Build timeline format expected by RoomView
    // Build timeline format expected by RoomView
const timeline = roomLogs.map(log => ({
  startEvent: log.startEvent,
  endEvent: log.endEvent
}));

    this.selectedRoom = {
      ...room,
      timeline
    };

    this.showRoomView = true;

  } catch (err) {}
},

    goBack() {
      this.showRoomView = false;
      this.selectedRoom = null;
    }
  },
  computed: {
  groupedRooms() {
  const buildingNames = {
    0: "A",
    1: "B",
    2: "C",
    3: "D",
    4: "E"
  };

  const groups = {};

  for (const r of this.rooms) {
    const key = buildingNames[r.building] ?? r.building;
    if (!groups[key]) groups[key] = [];
    groups[key].push(r);
  }

  // Turn into a sorted array: A, B, C, D, E
  return Object.keys(groups)
    .sort()                          // alphabetical order
    .map(key => ({
      building: key,
      rooms: groups[key]
    }));
}
  },
  mounted() {
    this.fetchRooms();
    // Optional: Refresh every 5 seconds
    setInterval(this.fetchRooms, 5000);
  }
};
