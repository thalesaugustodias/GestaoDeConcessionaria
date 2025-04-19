<template>
  <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1><i class="fas fa-industry mr-2"></i>Fabricantes</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item active">Fabricantes</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <div class="row mb-3">
        <div class="col-12">
          <router-link to="/fabricantes/create" class="btn btn-primary">
            <i class="fas fa-plus mr-1"></i>Cadastrar
          </router-link>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Lista de Fabricantes</h3>
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
                    <th>Nome</th>
                    <th>País</th>
                    <th>Ano</th>
                    <th>Website</th>
                    <th>Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="f in filteredItems" :key="f.id">
                    <td>{{ f.id }}</td>
                    <td>{{ f.nome }}</td>
                    <td>{{ f.paisOrigem }}</td>
                    <td>{{ f.anoFundacao }}</td>
                    <td><a :href="f.website" target="_blank">{{ f.website }}</a></td>
                    <td>
                      <div class="btn-group">
                        <router-link :to="`/fabricantes/${f.id}`" class="btn btn-info btn-sm">
                          <i class="fas fa-eye"></i>
                        </router-link>
                        <router-link :to="`/fabricantes/${f.id}/edit`" class="btn btn-warning btn-sm">
                          <i class="fas fa-edit"></i>
                        </router-link>
                        <button @click="excluir(f.id)" class="btn btn-danger btn-sm">
                          <i class="fas fa-trash"></i>
                        </button>
                      </div>
                    </td>
                  </tr>
                  <tr v-if="filteredItems.length === 0">
                    <td colspan="6" class="text-center">Nenhum fabricante encontrado</td>
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
    const fabricantes = ref<Array<any>>([]);
    const searchTerm = ref('');

    const filteredItems = computed(() => {
      if (!searchTerm.value) return fabricantes.value;
      const term = searchTerm.value.toLowerCase();
      return fabricantes.value.filter(item =>
        item.nome.toLowerCase().includes(term) ||
        item.paisOrigem.toLowerCase().includes(term) ||
        item.anoFundacao.toString().includes(term)
      );
    });

    const load = async () => {
      try {
        const resp = await api.get('/fabricantes');
        fabricantes.value = resp.data;
      } catch (error) {
        notifyError('Erro ao carregar fabricantes');
        console.error(error);
      }
    };

    const excluir = (id: number) => {
      Swal.fire({
        title: 'Confirmar Exclusão',
        text: 'Esta ação não pode ser desfeita.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#3085d6',
        confirmButtonText: 'Sim, excluir!',
        cancelButtonText: 'Cancelar'
      }).then(async result => {
        if (result.isConfirmed) {
          try {
            await api.delete(`/fabricantes/${id}`);
            await load();
            notifySuccess('Fabricante excluído com sucesso!');
          } catch (error) {
            notifyError('Erro ao excluir fabricante');
            console.error(error);
          }
        }
      });
    };

    onMounted(load);

    return { fabricantes, searchTerm, filteredItems, excluir };
  }
};
</script>
