import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import Login from '../views/Login.vue';
import Pedidos from '../views/Pedidos.vue';
import Perfil from '../views/Perfil.vue';
import Register from '../views/Register.vue';
import Blog from '../views/Blog.vue'; 
import Meds from '../views/Meds.vue';
import Oportunidades from '../views/Oportunidades.vue';
import Carrito from '../views/Carrito.vue';
import Tienda from '../views/Tienda.vue';
import Checkout from '../views/Checkout.vue';
import PrivateZone from '../views/PrivateZone.vue';


const routes = [
  { path: '/', component: Home, meta: { public: true } },
  { path: '/login', component: Login, meta: { public: true } },
  { path: '/pedidos', component: Pedidos, meta: { requiresAuth: true } },
  { path: '/perfil', component: Perfil, meta: { requiresAuth: true } },
  { path: '/register', component: Register, meta: { public: true } },
  { path: '/blog', component: Blog, meta: { public: true } },
  { path: '/medicamentos', component: Meds, meta: { public: true } },
  { path: '/oportunidades', component: Oportunidades, meta: { public: true } },
  { path: '/carrito', component: Carrito, meta: { public: true } },
  { path: '/tienda', component: Tienda, meta: { public: true } },
  { path: '/checkout', component: Checkout, meta: { public: true } },
  { path: '/privatezone', component: PrivateZone, meta: { public: true } }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

// Protegemos rutas privadas (esto se mejorará más adelante con autenticación)
// router.beforeEach((to, from, next) => {
//   const isAuthenticated = false; // Esto se conectará con Pinia después
//   if (to.meta.requiresAuth && !isAuthenticated) {
//     next('/login');
//   } else {
//     next();
//   }
// });

router.beforeEach((to, from, next) =>{
  const user = JSON.parse(localStorage.getItem('farmed_user') || '{}');
  const isAuthenticated = !!user.email;
  const isAdmin = user.rol === 'admin';
  if (to.meta.requiresAuth && !isAuthenticated) {
    next('/login');
  } else if (to.meta.requiresAdmin && !isAdmin) {
    next('/');
  } else {
    next();
  }
})

export default router;
