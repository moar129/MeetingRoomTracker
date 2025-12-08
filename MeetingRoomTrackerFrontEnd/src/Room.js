import template from './Room.html?raw'

export default {
  name: 'RoomView',
  props: {
    room: { type: Object, required: true }
  },
  template,
  methods: {
    formatDate(dt) {
      if (!dt) return ''
      const d = new Date(dt)
      return d.toLocaleString()
    },
    formatTime(dt) {
      if (!dt) return ''
      const d = new Date(dt)
      return d.toLocaleTimeString('da-DK', { hour: '2-digit', minute: '2-digit' })
    },
    getStatusLabel(status) {
      const statuses = { available: 'Ledig', occupied: 'Optaget', unknown: 'Mangler data' }
      return statuses[status] || statuses.unknown
    },
    getStatusClass(status) {
      return status || 'unknown'
    }
  }
}
