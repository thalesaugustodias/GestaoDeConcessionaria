<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1><i class="fas fa-shopping-cart mr-2"></i>Registrar Venda</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/vendas">Vendas</router-link></li>
            <li class="breadcrumb-item active">Registrar</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <form @submit.prevent="submit">
        <div class="card card-success">
          <div class="card-header">
            <h3 class="card-title">Dados da Venda</h3>
            <div class="card-tools">
              <router-link to="/vendas" class="btn btn-secondary btn-sm">
                <i class="fas fa-arrow-left"></i> Voltar
              </router-link>
            </div>
          </div>
          <div class="card-body">
            <div class="row">
              <div class="col-md-6">
                <div class="form-group">
                  <label>Veículo</label>
                  <select v-model="m.veiculoId"
                          @change="fetchPreco"
                          class="form-control"
                          :class="{'is-invalid': errors.veiculoId}"
                          required>
                    <option value="">Selecione um veículo</option>
                    <option v-for="v in veiculos" :key="v.id" :value="v.id">
                      {{ v.modelo }} ({{ v.anoFabricacao }}) - {{ formatCurrency(v.preco) }}
                    </option>
                  </select>
                  <div v-if="errors.veiculoId" class="invalid-feedback">{{ errors.veiculoId[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Concessionária</label>
                  <select v-model="m.concessionariaId"
                          class="form-control"
                          :class="{'is-invalid': errors.concessionariaId}"
                          required>
                    <option value="">Selecione uma concessionária</option>
                    <option v-for="c in concessionarias" :key="c.id" :value="c.id">{{ c.nome }}</option>
                  </select>
                  <div v-if="errors.concessionariaId" class="invalid-feedback">{{ errors.concessionariaId[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Cliente</label>
                  <select v-model="m.clienteId"
                          class="form-control"
                          :class="{'is-invalid': errors.clienteId}"
                          required>
                    <option value="">Selecione um cliente</option>
                    <option v-for="c in clientes" :key="c.id" :value="c.id">{{ c.nome }}</option>
                  </select>
                  <div v-if="errors.clienteId" class="invalid-feedback">{{ errors.clienteId[0] }}</div>
                </div>
              </div>

              <div class="col-md-6">
                <div class="form-group">
                  <label>Data da Venda</label>
                  <input v-model="m.dataVenda"
                         type="date"
                         class="form-control"
                         :class="{'is-invalid': errors.dataVenda}"
                         required />
                  <div v-if="errors.dataVenda" class="invalid-feedback">{{ errors.dataVenda[0] }}</div>
                </div>

                <div class="form-group">
                  <label>Preço de Venda</label>
                  <div class="input-group">
                    <div class="input-group-prepend">
                      <span class="input-group-text">R$</span>
                    </div>
                    <input v-model.number="m.precoVenda"
                           type="number"
                           class="form-control"
                           :class="{'is-invalid': errors.precoVenda}"
                           min="0"
                           step="0.01"
                           required />
                  </div>
                  <div v-if="errors.precoVenda" class="invalid-feedback">{{ errors.precoVenda[0] }}</div>
                </div>

                <div class="alert alert-info" v-if="descontoInfo">
                  <i class="fas fa-info-circle mr-1"></i> {{ descontoInfo }}
                </div>
              </div>
            </div>
          </div>
          <div class="card-footer">
            <button type="submit" class="btn btn-success float-right" :disabled="loading">
              <i v-if="loading" class="fas fa-spinner fa-spin"></i>
              <span v-else><i class="fas fa-check mr-1"></i>Registrar</span>
            </button>
          </div>
        </div>
      </form>
    </div>
  </section>
</template>

<script lang="ts">
import { reactive, ref, onMounted, computed } from 'vue';
import { useRouter } from 'vue-router';
import api from '@/services/api';
import { useServerValidation } from '@/composables/useServerValidation';
import { notifySuccess, notifyError } from '@/services/notificationService';

export default {
  name: 'Form',
  setup() {
    const router = useRouter();
    const loading = ref(false);
    const precoOriginal = ref(0);
    const m = reactive<any>({
      veiculoId: '',
      concessionariaId: '',
      clienteId: '',
      dataVenda: new Date().toISOString().substr(0, 10),
      precoVenda: 0
    });

    const veiculos = ref<any[]>([]);
    const concessionarias = ref<any[]>([]);
    const clientes = ref<any[]>([]);
    const { errors, clearErrors, handleError } = useServerValidation();

    const formatCurrency = (valor: number): string => {
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
      }).format(valor);
    };

    const descontoInfo = computed(() => {
      if (precoOriginal.value > 0 && m.precoVenda < precoOriginal.value) {
        const desconto = precoOriginal.value - m.precoVenda;
        const percentual = (desconto / precoOriginal.value) * 100;
        return `Desconto aplicado: ${formatCurrency(desconto)} (${percentual.toFixed(2)}%)`;
      }
      return '';
    });

    const fetchPreco = async () => {
      if (m.veiculoId) {
        try {
          const { data } = await api.get(`/veiculos/${m.veiculoId}`);
          m.precoVenda = data.preco;
          precoOriginal.value = data.preco;
        } catch (error) {
          notifyError('Erro ao buscar preço do veículo');
          console.error(error);
        }
      }
    };

    onMounted(async () => {
      try {
        const [{ data: v }, { data: con }, { data: cli }] = await Promise.all([
          api.get('/vendas/obter-dados-para-criacao'),
          api.get('/concessionarias'),
          api.get('/clientes')
        ]);
        veiculos.value = v.veiculos;
        concessionarias.value = con;
        clientes.value = cli;
      } catch (error) {
        notifyError('Erro ao carregar dados iniciais');
        console.error(error);
      }
    });

    const submit = async () => {
      clearErrors();
      loading.value = true;

      try {
        await api.post('/vendas', m);
        notifySuccess('Venda registrada com sucesso!');
        router.push('/vendas');
      } catch (error) {
        notifyError('Erro ao registrar venda');
        handleError(error);
      } finally {
        loading.value = false;
      }
    };

    return {
      m,
      veiculos,
      concessionarias,
      clientes,
      fetchPreco,
      submit,
      errors,
      loading,
      formatCurrency,
      descontoInfo
    };
  }
};
</script>
