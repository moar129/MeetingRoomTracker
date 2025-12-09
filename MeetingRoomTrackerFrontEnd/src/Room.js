import template from './Room.html?raw'

export default {
  template,
  name: "RoomView",

  props: {
    room: Object
  },

  computed: {
    buildingName() {
      const map = { 0: "A", 1: "B", 2: "C", 3: "D", 4: "E" }
      return map[this.room.building] ?? this.room.building
    },

    formattedTimeline() {
      if (!this.room.timeline) return []

      const out = []

      this.room.timeline.forEach(log => {
        // Start event
        out.push({
          time: this.formatTime(log.startEvent),
          label: "Møde startet"
        })

        // End event (if exists)
        if (log.endEvent) {
          out.push({
            time: this.formatTime(log.endEvent),
            label: "Møde afsluttet"
          })
        }
      })

      return out
    }
  },

  methods: {
    formatTime(dtString) {
      const dt = new Date(dtString)
      if (isNaN(dt)) return "—"

      return dt.toLocaleTimeString("da-DK", {
        hour: "2-digit",
        minute: "2-digit"
      })
    }
  }
}
