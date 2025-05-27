<template>
  <div id="app" class="wrapper">
    <!-- Navbar -->
    <NavBar v-if="isAuthenticated" />

    <!-- Main Sidebar Container -->
    <SideBar v-if="isAuthenticated" />

    <!-- Content Wrapper -->
    <div :class="{ 'content-wrapper': isAuthenticated }">
      <router-view />
    </div>

    <!-- Footer -->
    <AppFooter v-if="isAuthenticated" />
  </div>
</template>

<script lang="ts">
import { computed, onMounted } from 'vue';
import { useRouter } from 'vue-router';
import NavBar from '@/components/Navbar.vue';
import SideBar from '@/components/Sidebar.vue';
import AppFooter from '@/components/Footer.vue';

export default {
  name: 'App',
  components: {
    NavBar,
    SideBar,
    AppFooter
  },
  setup() {
    const router = useRouter();

    const isAuthenticated = computed(() => {
      const publicRoutes = ['/login'];
      const currentRoute = router.currentRoute.value.path;
      return !publicRoutes.includes(currentRoute) && !!localStorage.getItem('token');
    });

    onMounted(() => {
      const token = localStorage.getItem('token');
      if (!token && router.currentRoute.value.path !== '/login') {
        router.push('/login');
      }
    });

    return { isAuthenticated };
  }
};
</script>
