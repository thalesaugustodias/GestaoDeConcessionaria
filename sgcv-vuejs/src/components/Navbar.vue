<template>
  <nav class="main-header navbar navbar-expand navbar-white navbar-light">
    <!-- Left navbar links -->
    <ul class="navbar-nav">
      <li class="nav-item">
        <a class="nav-link" data-widget="pushmenu" href="#" role="button">
          <i class="fas fa-bars"></i>
        </a>
      </li>
    </ul>

    <!-- Right navbar links -->
    <ul class="navbar-nav ml-auto">
      <!-- Notifications Dropdown Menu -->
      <li class="nav-item dropdown">
        <a class="nav-link" data-toggle="dropdown" href="#" aria-expanded="false">
          <i class="far fa-bell"></i>
          <span v-if="notificationCount > 0" class="badge badge-warning navbar-badge">{{ notificationCount }}</span>
        </a>
        <div class="dropdown-menu dropdown-menu-lg dropdown-menu-right" style="left: inherit; right: 0px;">
          <span class="dropdown-item dropdown-header">{{ notificationCount }} Notificações</span>
          <div class="dropdown-divider"></div>
          <a v-for="(notification, index) in notifications" :key="index" href="#" class="dropdown-item">
            <i :class="notification.icon"></i> {{ notification.text }}
            <span class="float-right text-muted text-sm">{{ notification.time }}</span>
          </a>
          <div class="dropdown-divider"></div>
          <a href="#" class="dropdown-item dropdown-footer">Ver todas notificações</a>
        </div>
      </li>
      <!-- Profile Dropdown Menu -->
      <li class="nav-item dropdown">
        <a class="nav-link" data-toggle="dropdown" href="#">
          <i class="fas fa-user-circle"></i>
        </a>
        <div class="dropdown-menu dropdown-menu-right">
          <span class="dropdown-item dropdown-header">Opções de usuário</span>
          <div class="dropdown-divider"></div>
          <a href="#" class="dropdown-item">
            <i class="fas fa-user mr-2"></i> Perfil
          </a>
          <div class="dropdown-divider"></div>
          <a href="#" class="dropdown-item" @click.prevent="logout">
            <i class="fas fa-sign-out-alt mr-2"></i> Sair
          </a>
        </div>
      </li>
    </ul>
  </nav>
</template>

<script lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { notifyInfo } from '@/services/notificationService';

export default {
  name: 'NavBar',
  setup() {
    const router = useRouter();
    const notificationCount = ref(2);
    const notifications = ref([
      { icon: 'fas fa-envelope mr-2', text: 'Nova mensagem recebida', time: '3 mins' },
      { icon: 'fas fa-users mr-2', text: 'Novo cliente registrado', time: '12 horas' }
    ]);

    const logout = () => {
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      notifyInfo('Sessão encerrada com sucesso');
      router.push('/login');
    };

    return { logout, notificationCount, notifications };
  }
};
</script>
