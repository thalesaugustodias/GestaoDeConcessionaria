<template>
  <aside class="main-sidebar sidebar-dark-primary elevation-4">
    <a href="/dashboard" class="brand-link">
      <img src="/img/AdminLTELogo.png" alt="AdminLTE Logo" class="brand-image img-circle elevation-3" style="opacity: .8">
      <span class="brand-text font-weight-light">SGCV</span>
    </a>
    <div class="sidebar">
      <!-- Sidebar user panel (optional) -->
      <div class="user-panel mt-3 pb-3 mb-3 d-flex">
        <div class="image">
          <img src="/img/user2-160x160.jpg" class="img-circle elevation-2" alt="User Image">
        </div>
        <div class="info">
          <a href="#" class="d-block">{{ userName }}</a>
        </div>
      </div>

      <!-- Sidebar Menu -->
      <nav class="mt-2">
        <ul class="nav nav-pills nav-sidebar flex-column" data-widget="treeview" role="menu" data-accordion="false">
          <li v-for="item in menu" :key="item.path" class="nav-item">
            <router-link :to="item.path" class="nav-link" :class="{ active: currentPath === item.path }">
              <i :class="['nav-icon', item.icon]"></i>
              <p>{{ item.label }}</p>
            </router-link>
          </li>
        </ul>
      </nav>
    </div>
  </aside>
</template>

<script lang="ts">
import { computed, ref, onMounted, watch } from 'vue';
import { useRoute } from 'vue-router';

export default {
  name: 'SideBar',
  setup() {
    const route = useRoute();
    const currentPath = ref(route.path);

    // Atualiza o caminho atual quando a rota muda
    watch(() => route.path, (newPath) => {
      currentPath.value = newPath;
    });

    const token = localStorage.getItem('token') || '';
    const payload = token ? JSON.parse(atob(token.split('.')[1])) : {};
    const role = payload.role as string || '';
    const userName = computed(() => payload.name || 'Usuário');

    const all = [
      { path:'/dashboard',       label:'Dashboard',       icon:'fas fa-tachometer-alt', roles:['Administrador','Gerente','Vendedor'] },
      { path:'/fabricantes',     label:'Fabricantes',     icon:'fas fa-industry',       roles:['Administrador'] },
      { path:'/concessionarias', label:'Concessionárias', icon:'fas fa-building',       roles:['Administrador'] },
      { path:'/veiculos',        label:'Veículos',        icon:'fas fa-car',            roles:['Gerente'] },
      { path:'/clientes',        label:'Clientes',        icon:'fas fa-users',          roles:['Administrador','Gerente','Vendedor'] },
      { path:'/vendas',          label:'Vendas',          icon:'fas fa-shopping-cart',  roles:['Vendedor'] },
    ];

    const menu = computed(() => all.filter(i => i.roles.includes(role)));

    return { menu, currentPath, userName };
  }
};
</script>
