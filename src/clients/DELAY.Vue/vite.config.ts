import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import fs from "fs";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    host: true,
    port: 8043,
    watch: {
      usePolling: true,
    },
    https: {
      key: fs.readFileSync("./src/certs/localhost-key.pem"),
      cert: fs.readFileSync("./src/certs/localhost.pem"),
    },
  },
})
