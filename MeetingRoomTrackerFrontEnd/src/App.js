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
      statuses: {
        available: 'Ledig',
        occupied: 'Optaget',
        unknown: 'Mangler data'
      },
      selectedRoom: null,
      showRoomView: false,
      mockRooms: [
        { id: 1, name: 'Paris', roomNumber: 101, building: 'A', floor: 2, capacity: 8, status: false },
        { id: 2, name: 'London', roomNumber: 102, building: 'A', floor: 2, capacity: 6, status: false },
        { id: 3, name: 'New York', roomNumber: 103, building: 'A', floor: 2, capacity: 10, status: true },
        { id: 4, name: 'Berlin', roomNumber: 201, building: 'B', floor: 3, capacity: 12, status: false },
        { id: 5, name: 'Bern', roomNumber: 202, building: 'B', floor: 3, capacity: 8, status: true },
        { id: 6, name: 'Lisbon', roomNumber: 203, building: 'B', floor: 3, capacity: 6, status: true },
        { id: 7, name: 'Stockholm', roomNumber: 204, building: 'B', floor: 3, capacity: 10, status: true },
        { id: 8, name: 'Oslo', roomNumber: 205, building: 'B', floor: 3, capacity: 8, status: false },
        { id: 9, name: 'Helsinki', roomNumber: 206, building: 'B', floor: 3, capacity: 6, status: false },
        { id: 10, name: 'Copenhagen', roomNumber: 207, building: 'B', floor: 3, capacity: 10, status: false },
        { id: 11, name: 'Reykjavik', roomNumber: 208, building: 'B', floor: 3, capacity: 8, status: true },
        { id: 12, name: 'Dublin', roomNumber: 209, building: 'B', floor: 3, capacity: 6, status: false },
      ],
      mockTimeline: [
        { roomId: 1, timestamp: '2025-12-08T09:15:00', event: 'Møde startet' },
        { roomId: 1, timestamp: '2025-12-08T10:30:00', event: 'Møde afsluttet' },
        { roomId: 1, timestamp: '2025-12-08T13:00:00', event: 'Præsentation startet' },
        { roomId: 1, timestamp: '2025-12-08T14:45:00', event: 'Præsentation afsluttet' },
        { roomId: 1, timestamp: '2025-12-08T15:30:00', event: 'Workshop startet' }
      ]
    };
  },
  methods: {
    async fetchRoomStatuses() {
      try {
        const response = await fetch(`${this.apiBaseUrl}/api/rooms/statuses`);
        if (response.ok) {
          this.rooms = await response.json();
        } else {
          console.error('Fejl ved hentning af rumstatus:', response.status);
        }
      } catch (error) {
        console.error('Fejl ved håndtering af rumstatus:', error);
      }
    },
    getStatusLabel(status) {
      return this.statuses[status] || this.statuses.unknown;
    },
    getStatusClass(status) {
      return status || 'unknown';
    },
    openRoom(roomId) {
      const room = this.mockRooms.find(r => r.id === roomId);
      if (room) {
        this.selectedRoom = {
          ...room,
          timeline: this.mockTimeline.filter(t => t.roomId === roomId)
        };
        this.showRoomView = true;
      }
    },
    goBack() {
      this.showRoomView = false;
      this.selectedRoom = null;
    }
  },
  mounted() {
    this.fetchRoomStatuses();
    // Optional: Refresh every 5 seconds
    setInterval(this.fetchRoomStatuses, 5000);
  }
};
