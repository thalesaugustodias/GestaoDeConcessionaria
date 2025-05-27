<template>
  <footer class="main-footer text-center">
    <strong>&copy; {{ year }} SGCV.</strong>
    Todos os direitos reservados.
  </footer>
</template>

<script lang="ts">
import { ref } from 'vue';

export default {
  name: 'AppFooter',
  setup() {
    const year = ref(new Date().getFullYear());
    return { year };
  }
};
</script>
