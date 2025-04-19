<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1>
            <i :class="editing ? 'fas fa-edit' : 'fas fa-plus-circle'"></i>
            {{ editing ? 'Editar' : 'Cadastrar' }} Cliente
          </h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/clientes">Clientes</router-link></li>
            <li class="breadcrumb-item active">{{ editing ? 'Editar' : 'Cadastrar' }}</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <form @submit.prevent="submit">
        <div class="card card-primary">
          <div class="card-header">
            <h3 class="card-title">Dados do Cliente</h3>
            <div class="card-tools">
              <router-link to="/clientes" class="btn btn-secondary btn-sm">
                <i class="fas fa-arrow-left"></i> Voltar
              </router-link>
            </div>
          </div>
          <div class="card-body">
            <div class="form-group">
              <label>Nome</label>
              <input v-model="m.nome" class="form-control" :class="{'is-invalid': errors.nome}" required />
              <div v-if="errors.nome" class="invalid-feedback">{{ errors.nome[0] }}</div>
            </div>
            <div class="form-group">
              <label>CPF</label>
              <input v-model="m.cpf" v-validate-cpf class="form-control" :class="{'is-invalid': errors.cpf}" required />
              <div v-if="errors.cpf" class="invalid-feedback">{{ errors.cpf[0] }}</div>
            </div>
            <div class="form-group">
              <label>Telefone</label>
              <input v-model="m.telefone" v-phone class="form-control" :class="{'is-invalid': errors.telefone}" required />
              <div v-if="errors.telefone" class="invalid-feedback">{{ errors.telefone[0] }}</div>
            </div>
          </div>
          <div class="card-footer">
            <button type="submit" class="btn btn-success float-right" :disabled="loading">
              <i v-if="loading" class="fas fa-spinner fa-spin"></i>
              <i v-else class="fas fa-save"></i>
              Salvar
            </button>
          </div>
        </div>
      </form>
    </div>
  </section>
</template>

<script lang="ts">
import { reactive, ref, onMounted } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import api from '@/services/api';
import { useServerValidation } from '@/composables/useServerValidation';
import { notifySuccess, notifyError } from '@/services/notificationService';

export default {
  name: 'Form',
  setup() {
    const route = useRoute();
    const router = useRouter();
    const id = route.params.id as string | undefined;
    const editing = !!id;
    const loading = ref(false);
    const m = reactive<any>({ nome: '', cpf: '', telefone: '' });
    const { errors, clearErrors, handleError } = useServerValidation();

    onMounted(async () => {
      if (editing) {
        loading.value = true;
        try {
          const { data } = await api.get(`/clientes/${id}`);
          Object.assign(m, data);
        } catch (error) {
          notifyError('Erro ao carregar dados do cliente');
          handleError(error);
        } finally {
          loading.value = false;
        }
      }
    });

    const submit = async () => {
      clearErrors();
      loading.value = true;

      try {
        if (editing) {
          await api.put(`/clientes/${id}`, m);
          notifySuccess('Cliente atualizado com sucesso!');
        } else {
          await api.post('/clientes', m);
          notifySuccess('Cliente cadastrado com sucesso!');
        }
        router.push('/clientes');
      } catch (error) {
        notifyError('Erro ao salvar cliente');
        handleError(error);
      } finally {
        loading.value = false;
      }
    };

    return { m, editing, submit, errors, loading };
  }
};
</script>
