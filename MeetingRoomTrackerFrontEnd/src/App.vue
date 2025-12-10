<template>
    <div class="container py-5" style="max-width: 1000px; margin: 0 auto;">

        <!-- HEADER -->
        <header class="text-center mb-5">
            <h1 class="display-5 fw-bold header-accent">Meeting Room Tracker</h1>
        </header>

        <!-- INDEX / OVERVIEW -->
        <div v-if="!showRoomView">
            <!-- Global Stats -->
            <div class="row g-4 mb-5">
                <div class="col-md-4">
                    <div class="card text-center h-100 border-0">
                        <div class="card-body py-5">
                            <div class="display-2 fw-bold text-success">{{ globalFreeRooms }}</div>
                            <p class="fs-5 mt-2 mb-0">Ledige lokaler</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-center h-100 border-0">
                        <div class="card-body py-5">
                            <div class="display-2 fw-bold text-danger">{{ globalBusyRooms }}</div>
                            <p class="fs-5 mt-2 mb-0">Optaget</p>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="card text-center h-100 border-0">
                        <div class="card-body py-5">
                            <div class="display-2 fw-bold" style="color: var(--primary);">{{ globalTotalRooms }}</div>
                            <p class="fs-5 mt-2 mb-0">Total</p>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Buildings -->
            <BuildingOverview v-for="group in groupedRooms" :key="group.building" :group="group"
                @open-room="openRoom" />
        </div>

        <!-- ROOM DETAIL -->
        <div v-else>
            <a @click="goBack"
                class="d-inline-block mb-4 text-decoration-none hover-light px-3 py-2 rounded cursor-pointer">
                <i class="bi bi-arrow-left-circle me-2"></i> Tilbage
            </a>
            <RoomView :room="selectedRoom" />
        </div>

        <!-- PUT THIS AT THE VERY BOTTOM OF src/App.vue, inside the main <div class="container ..."> -->
        <footer class="mt-5 pt-4 text-center text-white">
            <p class="mb-2">
                <small>
                    Meeting Room Tracker er live opdateret oversigt over lokaler på skolen.
                </small>
            </p>
            <p>
                <a href="https://discord.gg/GARymCAQvG" target="_blank" class="text-decoration-none"
                    title="Join vores Discord">
                    <i class="bi bi-discord fs-4"></i>
                    <span class="ms-2">Join Discord</span>
                </a>
            </p>
            <p class="mb-2">
                <small>
                    © 2025 Mødelokaler
                </small>
            </p>
        </footer>
    </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
import BuildingOverview from './components/BuildingOverview.vue'
import RoomView from './components/RoomView.vue'

const rooms = ref([])
const selectedRoom = ref(null)
const showRoomView = ref(false)

const apiBaseUrl = import.meta.env.VITE_API_BASE_URL ||
    'https://roommeetingtracker-2025-win-exd2g5hagtb3gnfa.swedencentral-01.azurewebsites.net'

const fetchRooms = async () => {
    try {
        const res = await fetch(`${apiBaseUrl}/api/rooms`)
        if (res.ok) rooms.value = await res.json()
    } catch (e) { console.error(e) }
}

const openRoom = async (roomId) => {
    try {
        const [roomRes, logsRes] = await Promise.all([
            fetch(`${apiBaseUrl}/api/rooms/${roomId}`),
            fetch(`${apiBaseUrl}/api/timelog`)
        ])
        if (!roomRes.ok) return
        const room = await roomRes.json()
        const logs = logsRes.ok ? await logsRes.json() : []
        const timeline = logs.filter(l => l.roomId === roomId).map(l => ({
            startEvent: l.startEvent,
            endEvent: l.endEvent
        }))
        selectedRoom.value = { ...room, timeline }
        showRoomView.value = true
    } catch (e) { console.error(e) }
}

const goBack = () => {
    showRoomView.value = false
    selectedRoom.value = null
}

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

const globalFreeRooms = computed(() => rooms.value.filter(r => !r.status).length)
const globalBusyRooms = computed(() => rooms.value.filter(r => r.status).length)
const globalTotalRooms = computed(() => rooms.value.length)

onMounted(() => {
    fetchRooms()
    setInterval(fetchRooms, 5000)
})
</script>