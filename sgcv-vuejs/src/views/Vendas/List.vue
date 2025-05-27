<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1><i class="fas fa-shopping-cart mr-2"></i>Vendas</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item active">Vendas</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <div class="row mb-3">
        <div class="col-12">
          <router-link to="/vendas/create" class="btn btn-primary">
            <i class="fas fa-plus mr-1"></i>Registrar
          </router-link>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Lista de Vendas</h3>
              <div class="card-tools">
                <div class="input-group input-group-sm" style="width: 150px;">
                  <input type="text" v-model="searchTerm" class="form-control float-right" placeholder="Buscar">
                  <div class="input-group-append">
                    <button type="button" class="btn btn-default">
                      <i class="fas fa-search"></i>
                    </button>
                  </div>
                </div>
              </div>
            </div>
            <div class="card-body table-responsive p-0">
              <table class="table table-hover text-nowrap">
                <thead>
                  <tr>
                    <th>#</th>
                    <th>Veículo</th>
                    <th>Concessionária</th>
                    <th>Cliente</th>
                    <th>Data</th>
                    <th>Valor</th>
                    <th>Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="v in filteredItems" :key="v.id">
                    <td>{{ v.id }}</td>
                    <td>{{ v.veiculo.modelo }}</td>
                    <td>{{ v.concessionaria.nome }}</td>
                    <td>{{ v.cliente.nome }}</td>
                    <td>{{ formatDate(v.dataVenda) }}</td>
                    <td>{{ formatCurrency(v.precoVenda) }}</td>
                    <td>
                      <div class="btn-group">
                        <router-link :to="`/vendas/${v.id}`" class="btn btn-info btn-sm">
                          <i class="fas fa-eye"></i>
                        </router-link>
                      </div>
                    </td>
                  </tr>
                  <tr v-if="filteredItems.length === 0">
                    <td colspan="7" class="text-center">Nenhuma venda encontrada</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script lang="ts">
import { ref, onMounted, computed } from 'vue';
import api from '@/services/api';
import { notifyError } from '@/services/notificationService';

export default {
  name: 'List',
  setup() {
    const items = ref<any[]>([]);
    const searchTerm = ref('');

    const formatCurrency = (valor: number): string => {
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
      }).format(valor);
    };

    const formatDate = (date: string): string => {
      if (!date) return '';
      return new Date(date).toLocaleDateString('pt-BR');
    };

    const filteredItems = computed(() => {
      if (!searchTerm.value) return items.value;
      const term = searchTerm.value.toLowerCase();
      return items.value.filter(item =>
        item.veiculoModelo?.toLowerCase().includes(term) ||
        item.concessionariaNome?.toLowerCase().includes(term) ||
        item.clienteNome?.toLowerCase().includes(term)
      );
    });

    onMounted(async () => {
      try {
        const { data } = await api.get('/vendas');
        items.value = data;
      } catch (error) {
        notifyError('Erro ao carregar vendas');
        console.error(error);
      }
    });

    return { items, searchTerm, filteredItems, formatCurrency, formatDate };
  }
};
</script>
