<template>
    <div class="card mb-4"
        style="background: #1e1e2f; border: 1px solid #2a2a3d; border-radius: 1rem; overflow: hidden;">
        <!-- Building Header – Click to toggle -->
        <div class="d-flex justify-content-between align-items-center p-4 cursor-pointer" @click="isOpen = !isOpen"
            style="background: #161625;">
            <div class="d-flex align-items-center gap-3">
                <i class="bi bi-building fs-4" style="color: #5b8def;"></i>
                <div>
                    <h3 class="mb-0 fw-bold">Bygning {{ group.building }}</h3>
                    <small class="text-muted">{{ isOpen ? 'Skjul' : 'Vis' }} lokaler</small>
                </div>
            </div>

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

            <i :class="['bi fs-4 transition', isOpen ? 'bi-chevron-down' : 'bi-chevron-right']"></i>
        </div>

        <!-- Collapsible Room List -->
        <transition name="fade">
            <div v-if="isOpen" class="p-4 pt-0">
                <div v-for="room in group.rooms" :key="room.id"
                    class="d-flex justify-content-between align-items-center py-3 border-bottom border-secondary"
                    style="border-color: #2a2a3d !important;" @click="$emit('open-room', room.id)">
                    <div class="cursor-pointer">
                        <strong>{{ room.name }}</strong>
                        <small class="text-muted ms-2">#{{ room.roomNumber }}</small>
                    </div>
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
import { ref, computed } from 'vue'

const props = defineProps({ group: Object })
const emit = defineEmits(['open-room'])

const isOpen = ref(false)  // Start closed

const available = computed(() => props.group.rooms.filter(r => !r.status).length)
const occupied = computed(() => props.group.rooms.filter(r => r.status).length)
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
</style>