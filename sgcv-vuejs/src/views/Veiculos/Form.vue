<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1>
            <i :class="editing ? 'fas fa-edit' : 'fas fa-plus-circle'"></i>
            {{ editing ? 'Editar' : 'Cadastrar' }} Veículo
          </h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/veiculos">Veículos</router-link></li>
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
            <h3 class="card-title">Dados do Veículo</h3>
            <div class="card-tools">
              <router-link to="/veiculos" class="btn btn-secondary btn-sm">
                <i class="fas fa-arrow-left"></i> Voltar
              </router-link>
            </div>
          </div>
          <div class="card-body">
            <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label>Modelo</label>
                  <input v-model="m.modelo" class="form-control" :class="{'is-invalid': errors.modelo}" required />
                  <div v-if="errors.modelo" class="invalid-feedback">{{ errors.modelo[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Ano de Fabricação</label>
                  <input v-model.number="m.anoFabricacao"
                         type="number"
                         class="form-control"
                         :class="{'is-invalid': errors.anoFabricacao}"
                         min="1900"
                         max="2100"
                         required />
                  <div v-if="errors.anoFabricacao" class="invalid-feedback">{{ errors.anoFabricacao[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Preço</label>
                  <div class="input-group">
                    <div class="input-group-prepend">
                      <span class="input-group-text">R$</span>
                    </div>
                    <input v-model.number="m.preco"
                           type="number"
                           class="form-control"
                           :class="{'is-invalid': errors.preco}"
                           min="0"
                           step="0.01"
                           required />
                  </div>
                  <div v-if="errors.preco" class="invalid-feedback">{{ errors.preco[0] }}</div>
                </div>
              </div>
              <div class="col-md-6">
                <div class="form-group">
                  <label>Tipo</label>
                  <select v-model="m.tipo" class="form-control" :class="{'is-invalid': errors.tipo}" required>
                    <option value="">Selecione um tipo</option>
                    <option>Carro</option>
                    <option>Moto</option>
                    <option>Caminhão</option>
                  </select>
                  <div v-if="errors.tipo" class="invalid-feedback">{{ errors.tipo[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Fabricante</label>
                  <select v-model="m.fabricanteId" class="form-control" :class="{'is-invalid': errors.fabricanteId}" required>
                    <option value="">Selecione um fabricante</option>
                    <option v-for="f in fabricantes" :key="f.id" :value="f.id">{{ f.nome }}</option>
                  </select>
                  <div v-if="errors.fabricanteId" class="invalid-feedback">{{ errors.fabricanteId[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Descrição</label>
                  <textarea v-model="m.descricao"
                            class="form-control"
                            :class="{'is-invalid': errors.descricao}"
                            rows="3"></textarea>
                  <div v-if="errors.descricao" class="invalid-feedback">{{ errors.descricao[0] }}</div>
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
import { notifySuccess, notifyError } from '@/services/notificationService';

export default {
  name: 'Form',
  setup() {
    const route = useRoute();
    const router = useRouter();
    const id = route.params.id as string|undefined;
    const editing = !!id;
    const loading = ref(false);
    const m = reactive<any>({
      modelo: '',
      anoFabricacao: new Date().getFullYear(),
      preco: 0,
      tipo: '',
      fabricanteId: '',
      descricao: ''
    });
    const fabricantes = ref<any[]>([]);
    const { errors, clearErrors, handleError } = useServerValidation();

    onMounted(async () => {
      try {
        // Carregar fabricantes
        const { data } = await api.get('/fabricantes');
        fabricantes.value = data;

        // Se estiver editando, carregar dados do veículo
        if (editing) {
          const veiculoResp = await api.get(`/veiculos/${id}`);
          Object.assign(m, veiculoResp.data);
        }
      } catch (error) {
        notifyError('Erro ao carregar dados iniciais');
        console.error(error);
      }
    });

    const submit = async () => {
      clearErrors();
      loading.value = true;

      try {
        if (editing) {
          await api.put(`/veiculos/${id}`, m);
          notifySuccess('Veículo atualizado com sucesso!');
        } else {
          await api.post('/veiculos', m);
          notifySuccess('Veículo cadastrado com sucesso!');
        }
        router.push('/veiculos');
      } catch (error) {
        notifyError('Erro ao salvar veículo');
        handleError(error);
      } finally {
        loading.value = false;
      }
    };

    return { m, fabricantes, editing, submit, errors, loading };
  }
};
</script>
