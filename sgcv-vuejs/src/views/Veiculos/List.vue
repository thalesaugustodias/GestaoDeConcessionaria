<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1><i class="fas fa-car mr-2"></i>Veículos</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item active">Veículos</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <div class="row mb-3">
        <div class="col-12">
          <router-link to="/veiculos/create" class="btn btn-primary">
            <i class="fas fa-plus mr-1"></i>Cadastrar
          </router-link>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Lista de Veículos</h3>
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
                    <th>ID</th>
                    <th>Modelo</th>
                    <th>Ano</th>
                    <th>Tipo</th>
                    <th>Preço</th>
                    <th>Fabricante</th>
                    <th>Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="v in filteredItems" :key="v.id">
                    <td>{{ v.id }}</td>
                    <td>{{ v.modelo }}</td>
                    <td>{{ v.anoFabricacao }}</td>
                    <td>
                      <span :class="getBadgeClass(v.tipo)">{{ v.tipo }}</span>
                    </td>
                    <td>{{ formatCurrency(v.preco) }}</td>
                    <td>{{ v.nomeFabricante }}</td>
                    <td>
                      <div class="btn-group">
                        <router-link :to="`/veiculos/${v.id}`" class="btn btn-info btn-sm">
                          <i class="fas fa-eye"></i>
                        </router-link>
                        <router-link :to="`/veiculos/${v.id}/edit`" class="btn btn-warning btn-sm">
                          <i class="fas fa-edit"></i>
                        </router-link>
                        <button @click="del(v.id)" class="btn btn-danger btn-sm">
                          <i class="fas fa-trash"></i>
                        </button>
                      </div>
                    </td>
                  </tr>
                  <tr v-if="filteredItems.length === 0">
                    <td colspan="7" class="text-center">Nenhum veículo encontrado</td>
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
import Swal from 'sweetalert2';
import { notifySuccess, notifyError } from '@/services/notificationService';

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

    const getBadgeClass = (tipo: string): string => {
      switch (tipo) {
        case 'Carro':
          return 'badge badge-primary';
        case 'Moto':
          return 'badge badge-success';
        case 'Caminhão':
          return 'badge badge-warning';
        default:
          return 'badge badge-secondary';
      }
    };

    const filteredItems = computed(() => {
      if (!searchTerm.value) return items.value;
      const term = searchTerm.value.toLowerCase();
      return items.value.filter(item =>
        item.modelo.toLowerCase().includes(term) ||
        item.tipo.toLowerCase().includes(term) ||
        item.fabricanteNome?.toLowerCase().includes(term) ||
        String(item.anoFabricacao).includes(term)
      );
    });

    const load = async () => {
      try {
        const { data } = await api.get('/veiculos');
        items.value = data;
      } catch (error) {
        notifyError('Erro ao carregar veículos');
        console.error(error);
      }
    };

    const del = (id: number) => {
      Swal.fire({
        title: 'Excluir veículo?',
        text: 'Esta ação não pode ser revertida!',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar'
      }).then(async (result) => {
        if (result.isConfirmed) {
          try {
            await api.delete(`/veiculos/${id}`);
            await load();
            notifySuccess('Veículo excluído com sucesso!');
          } catch (error) {
            notifyError('Erro ao excluir veículo');
            console.error(error);
          }
        }
      });
    };

    onMounted(load);

    return { items, searchTerm, filteredItems, del, formatCurrency, getBadgeClass };
  }
};
</script>
