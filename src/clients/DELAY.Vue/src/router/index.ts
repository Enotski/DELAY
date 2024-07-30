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
      path: '/tickets',
      name: 'tickets',
      component: () => import('../views/tickets-view.vue')
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
      path: '/auth',
      name: 'auth',
      component: () => import('../views/auth-view.vue')
    },
    {
      path: '/users',
      name: 'users',
      component: () => import('../views/users-view.vue')
    },
  ]
})

export default router
