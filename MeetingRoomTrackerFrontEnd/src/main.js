// src/main.js
// This is the VERY FIRST file that runs when someone opens your website
// Its job is to "start" Vue and tell it: "Hey, take my App.vue and show it on the page!"

import { createApp } from 'vue'
// We import the function that creates a Vue app

import App from './App.vue'
// This is your main component — everything you see (header, buildings, rooms, footer) is inside App.vue

import './styles/app.css'
// This loads your beautiful dark design (colors, cards, fonts, Discord button, etc.)
// Without this line, the website would be white and ugly!

// Step 1: Create a new Vue app using the App.vue component
// Step 2: Attach ("mount") it to the <div id="app"></div> that exists in index.html
createApp(App).mount('#app')

// That's it! Only 4 lines of code start the entire website
// When someone visits your page:
// 1. Browser loads index.html
// 2. index.html loads this main.js file
// 3. This file starts Vue and shows your App.vue
// 4. Magic happens — your beautiful live-updating room tracker appears!