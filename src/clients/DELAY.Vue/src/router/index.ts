import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/home-view.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/boards',
      name: 'boards',
      component: () => import('../views/boards-view.vue')
    },
    {
      path: '/rooms',
      name: 'rooms',
      component: () => import('../views/rooms-view.vue')
    },
    {
      path: '/account',
      name: 'account',
      component: () => import('../views/account-view.vue')
    },
    {
      path: '/users',
      name: 'users',
      component: () => import('../views/users-view.vue')
    },
  ]
})

export default router;
