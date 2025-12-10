<!-- src/components/RoomView.vue -->
<!-- This component shows detailed info about ONE room + its history -->
<template>
  <div>
    <!-- ==================== ROOM INFO CARD ==================== -->
    <div class="card p-4 mb-4 position-relative" style="background:#1e1e2f; border:1px solid #2a2a3d;">

      <!-- Discord button in the top-right corner -->
      <a href="https://discord.gg/VyqGA4u7Ay" target="_blank" class="position-absolute end-0 top-0 mt-3 me-3"
        title="Join vores Discord-server">
        <i class="bi bi-discord fs-3 text-white" style="opacity:0.9;"></i>
      </a>

      <div class="d-flex align-items-start gap-4">
        <!-- Big blue door icon on the left -->
        <div class="text-primary">
          <i class="bi bi-door-open-fill fs-1"></i>
        </div>

        <!-- All the room details -->
        <div class="flex-grow-1">
          <h2 class="h3 fw-bold mb-1">{{ room.name }}</h2>
          <p class="text-white mb-3">Lokalenummer: #{{ room.roomNumber }}</p>

          <!-- Grid with 4 pieces of info -->
          <div class="row g-3">
            <div class="col-sm-6"><strong>Bygning:</strong> Bygning {{ buildingName }}</div>
            <div class="col-sm-6"><strong>Etage:</strong> {{ room.floor }}. sal</div>
            <div class="col-sm-6"><strong>Type:</strong> {{ roomTypeName }}</div>
            <div class="col-sm-6">
              <strong>Status:</strong>
              <!-- Green pill if free, red if busy -->
              <span class="px-4 py-2 rounded-pill fw-bold ms-2" :style="room.status
                ? 'background:#331f1f;color:#f87171'
                : 'background:#21332f;color:#4ade80'">
                {{ room.status ? 'Optaget' : 'Ledig' }}
              </span>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- ==================== HISTORY CARD ==================== -->
    <div class="card p-4" style="background:#1e1e2f; border:1px solid #2a2a3d; min-height:220px;">

      <h3 class="h5 fw-bold mb-4 d-flex align-items-center gap-2">
        <i class="bi bi-clock-history text-primary"></i> Historik i dag
      </h3>

      <!-- IF THERE IS HISTORY – show all events (newest first!) -->
      <div v-if="history.length" class="space-y-3">
        <div v-for="entry in history" :key="entry.time + entry.desc + Math.random()"
          class="d-flex justify-content-between align-items-center py-3 border-bottom" style="border-color:#2a2a3d;">

          <!-- Time + description -->
          <div>
            <strong class="fs-5">{{ entry.time }}</strong><br>
            <small class="text-secondary">{{ entry.desc }}</small>
          </div>

          <!-- Status badge (same colors as above) -->
          <span class="px-4 py-2 rounded-pill fw-bold" :style="entry.busy
            ? 'background:#331f1f;color:#f87171'
            : 'background:#21332f;color:#4ade80'">
            {{ entry.busy ? 'Optaget' : 'Ledig' }}
          </span>
        </div>
      </div>

      <!-- IF NO HISTORY – nice empty message -->
      <div v-else class="text-center py-8 text-white">
        <i class="bi bi-calendar-x fs-1 mb-4 opacity-50"></i>
        <p class="fs-5 mb-1">Ingen møder registreret i dag</p>
        <small>Dette lokale har været frit hele dagen</small>
      </div>
    </div>
  </div>
</template>

<script setup>
// We only need 'computed' from Vue here
import { computed } from 'vue'

// This component gets the full room object from App.vue
const props = defineProps({
  room: { type: Object, required: true }
})

// Convert building number (0-4) → A, B, C, D, E
const buildingName = computed(() => {
  const map = { 0: 'A', 1: 'B', 2: 'C', 3: 'D', 4: 'E' }
  return map[props.room.building] ?? props.room.building
})

// Convert roomType number → Danish name (matches your C# enum)
const roomTypeName = computed(() => {
  const types = ['Klasselokale', 'Mødelokale', 'Auditorium', 'Fællesområde']
  return types[props.room.roomType] || 'Ukendt type'
})

// Helper to format ISO time → "14:25"
const formatTime = (dateString) => {
  if (!dateString) return '—'
  return new Date(dateString).toLocaleTimeString('da-DK', {
    hour: '2-digit',
    minute: '2-digit'
  })
}

/* THIS IS THE MOST IMPORTANT PART – shows history with newest event at the top */
const history = computed(() => {
  // Make sure timeline exists and is an array
  const timeline = props.room.timeline || []
  if (!Array.isArray(timeline) || timeline.length === 0) return []

  const entries = []

  // Go through every log and create two entries (start + end)
  timeline.forEach(log => {
    if (log.startEvent) {
      entries.push({
        time: formatTime(log.startEvent),
        desc: 'Møde startet',
        busy: true,
        sortTime: log.startEvent  // keep original timestamp for accurate sorting
      })
    }
    if (log.endEvent) {
      entries.push({
        time: formatTime(log.endEvent),
        desc: 'Møde afsluttet',
        busy: false,
        sortTime: log.endEvent
      })
    }
  })

  // Sort: newest first. If same time → "afsluttet" comes before "startet"
  return entries.sort((a, b) => {
    if (a.sortTime === b.sortTime) {
      return a.busy ? 1 : -1   // end (false) before start (true)
    }
    return new Date(b.sortTime) - new Date(a.sortTime)
  })
})
</script>

<style scoped>
/* Small spacing helper – adds margin between history rows */
.space-y-3>*+* {
  margin-top: 0.75rem;
}
</style>