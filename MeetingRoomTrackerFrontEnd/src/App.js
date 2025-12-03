import appTemplate from './App.html?raw'

export default {
  template: appTemplate,
  name: 'App',
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
    }
  },
  mounted() {
    this.fetchRoomStatuses();
    // Optional: Refresh every 5 seconds
    setInterval(this.fetchRoomStatuses, 5000);
  }
};
