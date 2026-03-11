import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    hmr: {
      host: 'localhost',
      port: 3000,
      protocol: 'ws'
    },
    proxy: {
      '/blackjackhub': {
        target: 'https://localhost:5001',
        secure: false,
        ws: true
      },
      '/api': {
        target: 'https://localhost:5001',
        secure: false
      }
    }
  },
  build: {
    outDir: 'dist',
    sourcemap: true
  },
  test: {
    globals: true,
    environment: 'jsdom',
    setupFiles: './src/setupTests.js'
  }
});
