<!-- src/components/BuildingOverview.vue -->
<template>
    <!-- One card per building (A, B, C, D, E) -->
    <div class="card mb-4"
        style="background: #1e1e2f; border: 1px solid #2a2a3d; border-radius: 1rem; overflow: hidden;">

        <!-- CLICKABLE HEADER – click anywhere here to open/close the building -->
        <div id="CLickBuilding" class="d-flex justify-content-between align-items-center p-4 cursor-pointer" @click="isOpen = !isOpen"
            style="background: #161625;">

            <!-- Left side: Building icon + name + "Vis/Skjul lokaler" text -->
            <div class="d-flex align-items-center gap-3">
                <i class="bi bi-building fs-4" style="color: #5b8def;"></i>
                <div>
                    <h3 class="mb-0 fw-bold">Bygning {{ group.building }}</h3><br>
                    <!-- Changes text depending on whether it's open or closed -->
                    <small class="text-white">{{ isOpen ? 'Skjul' : 'Vis' }} lokaler</small>
                </div>
            </div>

            <!-- Middle: Live statistics – how many rooms are free/occupied -->
            <div class="d-flex gap-4 text-center">
                <div>
                    <div class="fs-3 fw-bold text-success">{{ available }}</div>
                    <small>Ledige</small>
                </div>
                <div>
                    <div class="fs-3 fw-bold text-danger">{{ occupied }}</div>
                    <small>Optaget</small>
                </div>
                <div>
                    <div class="fs-3 fw-bold" style="color: #5b8def;">{{ group.rooms.length }}</div>
                    <small>Total</small>
                </div>
            </div>

            <!-- Right side: Arrow that rotates when you click -->
            <i :class="['bi fs-4 transition', isOpen ? 'bi-chevron-down' : 'bi-chevron-right']"></i>
        </div>

        <!-- ROOM LIST – only visible when isOpen = true -->
        <transition name="fade">
            <div v-if="isOpen" class="p-4 pt-0">
                <!-- Loop through all rooms in this building -->
                <div v-for="room in group.rooms" :key="room.id"
                    class="d-flex justify-content-between align-items-center py-3 border-bottom border-secondary"
                    style="border-color: #2a2a3d !important;" @click="$emit('open-room', room.id)">
                    <!-- When clicked → go to room detail -->

                    <!-- Room name + number -->
                    <div class="cursor-pointer">
                        <strong>{{ room.name }}</strong>
                        <small class="text-muted ms-2">#{{ room.roomNumber }}</small>
                    </div>

                    <!-- Status badge – red if busy, green if free -->
                    <span class="px-4 py-2 rounded-pill fw-bold" :style="room.status
                        ? 'background: #331f1f; color: #f87171;'
                        : 'background: #21332f; color: #4ade80;'">
                        {{ room.status ? 'Optaget' : 'Ledig' }}
                    </span>
                </div>
            </div>
        </transition>
    </div>
</template>

<script setup>
// Import what we need from Vue
import { ref, computed } from 'vue'

// This component receives a "group" (one building + its rooms) from App.vue
const props = defineProps({
    group: Object
})

// We tell the parent (App.vue) when a room is clicked
const emit = defineEmits(['open-room'])

// Controls if the building is open or closed. false = starts closed
const isOpen = ref(false)

// Counts how many rooms are free in this building
const available = computed(() =>
    props.group.rooms.filter(room => !room.status).length
)

// Counts how many rooms are occupied
const occupied = computed(() =>
    props.group.rooms.filter(room => room.status).length
)
</script>

<style scoped>
/* Makes the arrow rotate smoothly */
.transition {
    transition: transform 0.3s ease;
}

.bi-chevron-down {
    transform: rotate(180deg);
}

/* Smooth fade + slide animation when opening/closing */
.fade-enter-active,
.fade-leave-active {
    transition: all 0.4s ease;
    max-height: 1000px;
}

.fade-enter-from,
.fade-leave-to {
    opacity: 0;
    max-height: 0;
    padding-top: 0 !important;
    padding-bottom: 0 !important;
}

/* Makes the whole header clickable */
.cursor-pointer {
    cursor: pointer;
}
</style>