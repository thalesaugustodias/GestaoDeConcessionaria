<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1>
            <i :class="editing ? 'fas fa-edit' : 'fas fa-plus-circle'"></i>
            {{ editing ? 'Editar' : 'Cadastrar' }} Concessionária
          </h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/concessionarias">Concessionárias</router-link></li>
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
            <h3 class="card-title">Dados da Concessionária</h3>
            <div class="card-tools">
              <router-link to="/concessionarias" class="btn btn-secondary btn-sm">
                <i class="fas fa-arrow-left"></i> Voltar
              </router-link>
            </div>
          </div>
          <div class="card-body">
            <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label>Nome</label>
                  <input v-model="m.nome" class="form-control" :class="{'is-invalid': errors.nome}" required />
                  <div v-if="errors.nome" class="invalid-feedback">{{ errors.nome[0] }}</div>
                </div>

                <div class="form-group">
                  <label>CEP</label>
                  <div class="input-group">
                    <input v-model="m.cep" class="form-control" :class="{'is-invalid': errors.cep}" @blur="buscarCep" required />
                    <div class="input-group-append">
                      <button type="button" class="btn btn-info" @click="buscarCep" :disabled="cepLoading">
                        <i v-if="cepLoading" class="fas fa-spinner fa-spin"></i>
                        <i v-else class="fas fa-search"></i>
                      </button>
                    </div>
                  </div>
                  <div v-if="errors.cep" class="invalid-feedback">{{ errors.cep[0] }}</div>
                  <small class="form-text text-muted">Digite o CEP para autopreenchimento</small>
                </div>

                <div class="form-group">
                  <label>Rua</label>
                  <input v-model="m.rua" class="form-control" :class="{'is-invalid': errors.rua}" required />
                  <div v-if="errors.rua" class="invalid-feedback">{{ errors.rua[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Cidade</label>
                  <input v-model="m.cidade" class="form-control" :class="{'is-invalid': errors.cidade}" required />
                  <div v-if="errors.cidade" class="invalid-feedback">{{ errors.cidade[0] }}</div>
                </div>
              </div>

              <div class="col-md-6">
                <div class="form-group">
                  <label>Estado</label>
                  <input v-model="m.estado" class="form-control" :class="{'is-invalid': errors.estado}" required />
                  <div v-if="errors.estado" class="invalid-feedback">{{ errors.estado[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Telefone</label>
                  <input v-model="m.telefone" v-phone class="form-control" :class="{'is-invalid': errors.telefone}" required />
                  <div v-if="errors.telefone" class="invalid-feedback">{{ errors.telefone[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Email</label>
                  <input v-model="m.email" type="email" class="form-control" :class="{'is-invalid': errors.email}" />
                  <div v-if="errors.email" class="invalid-feedback">{{ errors.email[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Capacidade Máxima de Veículos</label>
                  <input v-model.number="m.capacidadeMaximaVeiculos" type="number" class="form-control" :class="{'is-invalid': errors.capacidadeMaximaVeiculos}" min="1" required />
                  <div v-if="errors.capacidadeMaximaVeiculos" class="invalid-feedback">{{ errors.capacidadeMaximaVeiculos[0] }}</div>
                </div>
              </div>
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
import { notifySuccess, notifyError, notifyWarning } from '@/services/notificationService';
import { buscarCep as buscarCepService } from '@/services/cepService';

export default {
  name: 'Form',
  setup() {
    const route = useRoute();
    const router = useRouter();
    const id = route.params.id as string|undefined;
    const editing = !!id;
    const loading = ref(false);
    const cepLoading = ref(false);

    const m = reactive<any>({
      nome: '',
      cep: '',
      rua: '',
      cidade: '',
      estado: '',
      telefone: '',
      email: '',
      capacidadeMaximaVeiculos: 1
    });

    const { errors, clearErrors, handleError } = useServerValidation();

    const buscarCep = async () => {
      if (!m.cep || m.cep.replace(/\D/g, '').length < 8) {
        notifyWarning('Digite um CEP completo para buscar');
        return;
      }

      cepLoading.value = true;

      try {
        const cepData = await buscarCepService(m.cep);
        m.rua = cepData.logradouro;
        m.cidade = cepData.localidade;
        m.estado = cepData.uf;
        notifySuccess('Endereço preenchido com sucesso!');
      } catch (error: any) {
        if (error.response && error.response.status === 400) {
          notifyError('CEP não encontrado ou inválido');
        } else {
          notifyError('Erro ao buscar CEP: ' + (error.message || 'Verifique sua conexão'));
        }
      } finally {
        cepLoading.value = false;
      }
    };

    onMounted(async () => {
      if (editing) {
        loading.value = true;
        try {
          const { data } = await api.get(`/concessionarias/${id}`);
          Object.assign(m, data);
        } catch (error) {
          notifyError('Erro ao carregar dados da concessionária');
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
          await api.put(`/concessionarias/${id}`, m);
          notifySuccess('Concessionária atualizada com sucesso!');
        } else {
          await api.post('/concessionarias', m);
          notifySuccess('Concessionária cadastrada com sucesso!');
        }
        router.push('/concessionarias');
      } catch (error) {
        notifyError('Erro ao salvar concessionária');
        handleError(error);
      } finally {
        loading.value = false;
      }
    };

    return {
      m,
      editing,
      submit,
      errors,
      buscarCep,
      loading,
      cepLoading
    };
  }
};
</script>
