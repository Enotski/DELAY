import { fileURLToPath, URL } from 'node:url'

import { defineConfig } from 'vite'
import mkcert from'vite-plugin-mkcert'
import vueJsx from '@vitejs/plugin-vue-jsx';
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueJsx(),
    mkcert()
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    host: true,
    port: 443,
    // Для docker заккоментить
    watch: {
      usePolling: true,
    },
  },
})
