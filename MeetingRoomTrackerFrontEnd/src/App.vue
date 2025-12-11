<!-- src/App.vue -->
<!-- This is the MAIN component that shows everything: header, buildings list, room details, and footer -->
<template>
    <!-- Main container that centers everything and adds padding -->
    <div class="container py-5" style="max-width: 1000px; margin: 0 auto;">

        <!-- ==================== HEADER ==================== -->
        <header class="text-center mb-5">
            <!-- Big title of the app -->
            <a href="index.html" @click="goBack" class="text-decoration-none" style="cursor: pointer;">
                <h1 class="display-5 fw-bold header-accent">Meeting Room Tracker</h1>
            </a>
        </header>

        <!-- ==================== OVERVIEW PAGE (shows all buildings) ==================== -->
        <div v-if="!showRoomView">
            <!-- Global stats: Total free, busy, and total rooms across ALL buildings -->
            <div class="row g-4 mb-5">
                <!-- Free rooms card -->
                <div class="col-md-4">
                    <div class="card text-center h-100 border-0">
                        <div class="card-body py-5">
                            <div class="display-2 fw-bold text-success">{{ globalFreeRooms }}</div>
                            <p class="fs-5 mt-2 mb-0">Ledige lokaler</p>
                        </div>
                    </div>
                </div>
                <!-- Busy rooms card -->
                <div class="col-md-4">
                    <div class="card text-center h-100 border-0">
                        <div class="card-body py-5">
                            <div class="display-2 fw-bold text-danger">{{ globalBusyRooms }}</div>
                            <p class="fs-5 mt-2 mb-0">Optaget</p>
                        </div>
                    </div>
                </div>
                <!-- Total rooms card -->
                <div class="col-md-4">
                    <div class="card text-center h-100 border-0">
                        <div class="card-body py-5">
                            <div class="display-2 fw-bold" style="color: var(--primary);">{{ globalTotalRooms }}</div>
                            <p class="fs-5 mt-2 mb-0">Total</p>
                        </div>
                    </div>
                </div>
            </div>

            <!-- List of ALL buildings (A, B, C, etc.) – each is a separate component -->
            <BuildingOverview v-for="group in groupedRooms" :key="group.building" :group="group"
                @open-room="openRoom" />
        </div>

        <!-- ==================== ROOM DETAIL PAGE ==================== -->
        <div v-else>
            <!-- Back button to go to overview -->
            <a @click="goBack"
                class="d-inline-block mb-4 text-decoration-none hover-light px-3 py-2 rounded cursor-pointer">
                <i class="bi bi-arrow-left-circle me-2"></i> Tilbage
            </a>

            <!-- Shows the selected room's details -->
            <RoomView :room="selectedRoom" />
        </div>

        <!-- ==================== FOOTER ==================== -->
        <footer class="mt-5 pt-4 text-center text-white">

            <!-- Modern Discord Footer Banner (Entire Banner Is Clickable) -->
            <a href="https://discord.gg/GARymCAQvG" target="_blank" class="text-decoration-none text-white"
                style="display: block;">
                <div class="mt-3 p-3 text-white rounded-4" style="background-color: #5865F2; border-radius: 16px;">
                    <!-- Centered Discord CTA -->
                    <div class="d-flex flex-column align-items-center justify-content-center mb-2 text-center">
                        <i class="bi bi-discord fs-3 mb-1"></i>
                        <span class="fw-bold" style="font-size: 1rem;">Join vores Discord</span>
                    </div>

                    <!-- App description -->
                    <p class="mb-2 mt-2 text-center">
                        <small>
                            Meeting Room Tracker er en live opdateret oversigt over ledige lokaler på skolen.
                        </small>
                    </p>

                    <!-- Copyright -->
                    <p class="mb-0 text-center">
                        <small>© 2025 Mødelokaler</small>
                    </p>
                </div>
            </a>

        </footer>
    </div>
</template>

<script setup>
// Import what we need from Vue
import { ref, computed, onMounted } from 'vue'

// Import our child components
import BuildingOverview from './components/BuildingOverview.vue'
import RoomView from './components/RoomView.vue'

// Reactive variables (these change and update the screen automatically)
const rooms = ref([])              // All rooms from the API
const selectedRoom = ref(null)     // The room user clicked on
const showRoomView = ref(false)    // Are we showing overview or room detail?

// API URL (change this if your server is different)
const apiBaseUrl = import.meta.env.VITE_API_BASE_URL ||
    'https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net'

// Function to get ALL rooms from the server
const fetchRooms = async () => {
    try {
        const res = await fetch(`${apiBaseUrl}/api/rooms`)
        if (res.ok) rooms.value = await res.json()  // Save the rooms
    } catch (e) {
        console.error('Fejl ved hentning af lokaler:', e)
    }
}

// When user clicks a room → fetch its details + history
const openRoom = async (roomId) => {
    try {
        // Get room info + all timelogs
        const [roomRes, logsRes] = await Promise.all([
            fetch(`${apiBaseUrl}/api/rooms/${roomId}`),
            fetch(`${apiBaseUrl}/api/timelog`)
        ])

        if (!roomRes.ok) return  // If room not found → do nothing

        const room = await roomRes.json()  // Room details
        const logs = logsRes.ok ? await logsRes.json() : []  // All timelogs

        // Filter timelogs for THIS room only
        const timeline = logs
            .filter(log => log.roomId === roomId)
            .map(log => ({
                startEvent: log.startEvent,
                endEvent: log.endEvent
            }))

        selectedRoom.value = { ...room, timeline }  // Save room + its history
        showRoomView.value = true  // Switch to detail view
    } catch (e) {
        console.error('Fejl ved hentning af lokale:', e)
    }
}

// Back button → go back to overview
const goBack = () => {
    showRoomView.value = false
    selectedRoom.value = null
}

// Group rooms by building (A, B, C, etc.)
const groupedRooms = computed(() => {
    const map = { 0: 'A', 1: 'B', 2: 'C', 3: 'D', 4: 'E' }
    const groups = {}
    rooms.value.forEach(r => {
        const key = map[r.building] ?? r.building
        if (!groups[key]) groups[key] = []
        groups[key].push(r)
    })
    return Object.keys(groups).sort().map(key => ({ building: key, rooms: groups[key] }))
})

// Total free rooms across ALL buildings
const globalFreeRooms = computed(() => rooms.value.filter(r => !r.status).length)

// Total busy rooms
const globalBusyRooms = computed(() => rooms.value.filter(r => r.status).length)

// Total rooms
const globalTotalRooms = computed(() => rooms.value.length)

// When the page loads → get data and refresh every 5 seconds
onMounted(() => {
    fetchRooms()                     // Load data immediately
    setInterval(fetchRooms, 1000)    // Refresh every 1 seconds (1000 ms)
})
</script>

<style scoped>
/* Optional: Add any styles just for this component here */
</style>