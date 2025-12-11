<!-- src/components/BuildingOverview.vue -->
<template>
    <!-- One card per building (A, B, C, D, E) -->
    <div class="card mb-4"
        style="background: #1e1e2f; border: 1px solid #2a2a3d; border-radius: 1rem; overflow: hidden;">

        <!-- CLICKABLE HEADER – click anywhere here to open/close the building -->
        <div id="CLickBuilding" class="d-flex justify-content-between align-items-center p-4 cursor-pointer"
            @click="isOpen = !isOpen" style="background: #161625;" :data-testid="'building-' + group.building">

            <!-- Left side: Building icon + name + "Vis/Skjul lokaler" text -->
            <div class="d-flex align-items-center gap-3">
                <i class="bi bi-building fs-4" style="color: #5b8def;"></i>
                <div>
                    <h3 class="mb-0 fw-bold">Bygning {{ group.building }}</h3><br>
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

                <!-- Loop through all rooms in sorted order -->
                <div v-for="room in sortedRooms" :key="room.id"
                    class="d-flex justify-content-between align-items-center py-3 border-bottom border-secondary"
                    style="border-color: #2a2a3d !important;" @click="$emit('open-room', room.id)" :data-testid="'room-' + room.id">

                    <!-- Room name + number -->
                    <div class="cursor-pointer">
                        <strong>{{ room.name }}</strong>
                        <small class="text-muted ms-2">#{{ room.roomNumber }}</small>
                    </div>

                    <!-- Status badge – red if busy, green if free -->
                    <span class="px-4 py-2 rounded-pill fw-bold" :data-testid="'room-status-' + room.id" :style="room.status
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
import { ref, computed } from 'vue'

const props = defineProps({
    group: Object
})

const emit = defineEmits(['open-room'])

const isOpen = ref(false)

const available = computed(() =>
    props.group.rooms.filter(room => !room.status).length
)

const occupied = computed(() =>
    props.group.rooms.filter(room => room.status).length
)

// NEW: Sort free rooms first, then alphabetically by name
const sortedRooms = computed(() => {
    return [...props.group.rooms].sort((a, b) => {
        if (a.status !== b.status) {
            return a.status - b.status  // free (false) first
        }
        return a.name.localeCompare(b.name)
    })
})
</script>

<style scoped>
.transition {
    transition: transform 0.3s ease;
}

.bi-chevron-down {
    transform: rotate(180deg);
}

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

.cursor-pointer {
    cursor: pointer;
}
</style>
