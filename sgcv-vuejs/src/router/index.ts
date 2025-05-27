import { createRouter, createWebHistory } from 'vue-router';
import type { RouteRecordRaw } from 'vue-router';

// Components
import Dashboard from '@/views/Dashboard/Dashboard.vue';
import Login from '@/views/Login/Login.vue';

// Fabricantes
import FabricanteList from '@/views/Fabricantes/List.vue';
import FabricanteForm from '@/views/Fabricantes/Form.vue';
import FabricanteDetail from '@/views/Fabricantes/Details.vue';

// Concessionárias
import ConcessionariaList from '@/views/Concessionarias/List.vue';
import ConcessionariaForm from '@/views/Concessionarias/Form.vue';
import ConcessionariaDetail from '@/views/Concessionarias/Details.vue';

// Veículos
import VeiculoList from '@/views/Veiculos/List.vue';
import VeiculoForm from '@/views/Veiculos/Form.vue';
import VeiculoDetail from '@/views/Veiculos/Details.vue';

// Clientes
import ClienteList from '@/views/Clientes/List.vue';
import ClienteForm from '@/views/Clientes/Form.vue';
import ClienteDetail from '@/views/Clientes/Details.vue';

// Vendas
import VendaList from '@/views/Vendas/List.vue';
import VendaForm from '@/views/Vendas/Form.vue';
import VendaDetail from '@/views/Vendas/Details.vue';

const routes: RouteRecordRaw[] = [
  {
    path: '/login',
    name: 'Login',
    component: Login,
    meta: { public: true }
  },
  {
    path: '/',
    redirect: '/dashboard'
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Dashboard,
    meta: { roles: ['Administrador', 'Gerente', 'Vendedor'] }
  },

  // Fabricantes
  {
    path: '/fabricantes',
    name: 'FabricanteList',
    component: FabricanteList,
    meta: { roles: ['Administrador'] }
  },
  {
    path: '/fabricantes/create',
    name: 'FabricanteCreate',
    component: FabricanteForm,
    meta: { roles: ['Administrador'] }
  },
  {
    path: '/fabricantes/:id(\\d+)',
    name: 'FabricanteDetail',
    component: FabricanteDetail,
    meta: { roles: ['Administrador'] }
  },
  {
    path: '/fabricantes/:id(\\d+)/edit',
    name: 'FabricanteEdit',
    component: FabricanteForm,
    meta: { roles: ['Administrador'] }
  },

  // Concessionárias
  {
    path: '/concessionarias',
    name: 'ConcessionariaList',
    component: ConcessionariaList,
    meta: { roles: ['Administrador'] }
  },
  {
    path: '/concessionarias/create',
    name: 'ConcessionariaCreate',
    component: ConcessionariaForm,
    meta: { roles: ['Administrador'] }
  },
  {
    path: '/concessionarias/:id(\\d+)',
    name: 'ConcessionariaDetail',
    component: ConcessionariaDetail,
    meta: { roles: ['Administrador'] }
  },
  {
    path: '/concessionarias/:id(\\d+)/edit',
    name: 'ConcessionariaEdit',
    component: ConcessionariaForm,
    meta: { roles: ['Administrador'] }
  },

  // Veículos
  {
    path: '/veiculos',
    name: 'VeiculoList',
    component: VeiculoList,
    meta: { roles: ['Gerente'] }
  },
  {
    path: '/veiculos/create',
    name: 'VeiculoCreate',
    component: VeiculoForm,
    meta: { roles: ['Gerente'] }
  },
  {
    path: '/veiculos/:id(\\d+)',
    name: 'VeiculoDetail',
    component: VeiculoDetail,
    meta: { roles: ['Gerente'] }
  },
  {
    path: '/veiculos/:id(\\d+)/edit',
    name: 'VeiculoEdit',
    component: VeiculoForm,
    meta: { roles: ['Gerente'] }
  },

  // Clientes
  {
    path: '/clientes',
    name: 'ClienteList',
    component: ClienteList,
    meta: { roles: ['Administrador','Gerente','Vendedor'] }
  },
  {
    path: '/clientes/create',
    name: 'ClienteCreate',
    component: ClienteForm,
    meta: { roles: ['Administrador','Gerente','Vendedor'] }
  },
  {
    path: '/clientes/:id(\\d+)',
    name: 'ClienteDetail',
    component: ClienteDetail,
    meta: { roles: ['Administrador','Gerente','Vendedor'] }
  },
  {
    path: '/clientes/:id(\\d+)/edit',
    name: 'ClienteEdit',
    component: ClienteForm,
    meta: { roles: ['Administrador','Gerente','Vendedor'] }
  },

  // Vendas
  {
    path: '/vendas',
    name: 'VendaList',
    component: VendaList,
    meta: { roles: ['Vendedor'] }
  },
  {
    path: '/vendas/create',
    name: 'VendaCreate',
    component: VendaForm,
    meta: { roles: ['Vendedor'] }
  },
  {
    path: '/vendas/:id(\\d+)',
    name: 'VendaDetail',
    component: VendaDetail,
    meta: { roles: ['Vendedor'] }
  },
]

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
  linkActiveClass: 'active'
})

router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token')
  const isAuthenticated = !!token
  const isPublic = to.meta.public === true

  if (!isAuthenticated && !isPublic) {
    return next({ name: 'Login' })
  }
  if (isAuthenticated && to.name === 'Login') {
    return next({ name: 'Dashboard' })
  }

  // verifica role
  if (to.meta.roles && token) {
    const payload = JSON.parse(atob(token.split('.')[1]))
    if (!to.meta.roles.includes(payload.role)) {
      return next({ name: 'Login' })
    }
  }

  next()
})

export default router
