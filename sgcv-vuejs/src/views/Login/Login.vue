<template>
  <div class="login-page bg-light">
    <div class="login-box">
      <div class="card card-outline card-primary">
        <div class="card-header text-center">
          <a href="/" class="h1"><b>SGCV</b></a>
        </div>
        <div class="card-body">
          <p class="login-box-msg">Faça login para iniciar sua sessão</p>

          <form @submit.prevent="submit">
            <div class="input-group mb-3">
              <input v-model="user.nomeUsuario"
                     type="text"
                     class="form-control"
                     placeholder="Usuário"
                     :class="{'is-invalid': loginError}"
                     required />
              <div class="input-group-append">
                <div class="input-group-text">
                  <i class="fas fa-user"></i>
                </div>
              </div>
            </div>

            <div class="input-group mb-3">
              <input v-model="user.senha"
                     type="password"
                     class="form-control"
                     placeholder="Senha"
                     :class="{'is-invalid': loginError}"
                     required />
              <div class="input-group-append">
                <div class="input-group-text">
                  <i class="fas fa-lock"></i>
                </div>
              </div>
              <div v-if="loginError" class="invalid-feedback d-block">
                Usuário ou senha inválidos
              </div>
            </div>

            <div class="row">
              <div class="col-8">
                <div class="icheck-primary">
                  <input type="checkbox" id="remember" v-model="remember">
                  <label for="remember">Lembrar-me</label>
                </div>
              </div>
              <div class="col-4">
                <button type="submit" class="btn btn-primary btn-block" :disabled="loading">
                  <i v-if="loading" class="fas fa-spinner fa-spin"></i>
                  <span v-else>Entrar</span>
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { reactive, ref } from 'vue';
import { useRouter } from 'vue-router';
import api from '@/services/api';
import { notifySuccess, notifyError } from '@/services/notificationService';

export default {
  name: 'Login',
  setup() {
    const router = useRouter();
    const user = reactive({ nomeUsuario: '', senha: '' });
    const remember = ref(false);
    const loading = ref(false);
    const loginError = ref(false);

    const submit = async () => {
      loginError.value = false;
      loading.value = true;

      try {
        const resp = await api.post('/auth/login', user);
        const token = resp.data.token;

        localStorage.setItem('token', token);

        const payload = JSON.parse(atob(token.split('.')[1]));
        const role = (payload.role as string) || '';
        localStorage.setItem('role', role);

        if (remember.value) {
          localStorage.setItem('remember', 'true');
        } else {
          localStorage.removeItem('remember');
        }

        notifySuccess('Login realizado com sucesso!');
        router.push('/dashboard');
      } catch (error) {
        console.error('Erro no login:', error);
        loginError.value = true;
        notifyError('Falha no login. Verifique suas credenciais.');
      } finally {
        loading.value = false;
      }
    };

    return { user, remember, loading, loginError, submit };
  }
};
</script>

<style scoped>
  .login-page {
    height: 100vh;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .login-box {
    width: 360px;
  }

  @media (max-width: 576px) {
    .login-box {
      width: 90%;
    }
  }
</style>
