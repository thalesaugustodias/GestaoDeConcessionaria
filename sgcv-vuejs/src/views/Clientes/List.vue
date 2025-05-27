<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1><i class="fas fa-users mr-2"></i>Clientes</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item active">Clientes</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <div class="row mb-3">
        <div class="col-12">
          <router-link to="/clientes/create" class="btn btn-primary">
            <i class="fas fa-plus mr-1"></i>Cadastrar
          </router-link>
        </div>
      </div>

      <div class="row">
        <div class="col-12">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Lista de Clientes</h3>
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
                    <th>CPF</th>
                    <th>Telefone</th>
                    <th>Ações</th>
                  </tr>
                </thead>
                <tbody>
                  <tr v-for="c in filteredItems" :key="c.id">
                    <td>{{ c.id }}</td>
                    <td>{{ c.nome }}</td>
                    <td>{{ formatCpf(c.cpf) }}</td>
                    <td>{{ formatPhone(c.telefone) }}</td>
                    <td>
                      <div class="btn-group">
                        <router-link :to="`/clientes/${c.id}`" class="btn btn-info btn-sm">
                          <i class="fas fa-eye"></i>
                        </router-link>
                        <router-link :to="`/clientes/${c.id}/edit`" class="btn btn-warning btn-sm">
                          <i class="fas fa-edit"></i>
                        </router-link>
                        <button @click="del(c.id)" class="btn btn-danger btn-sm">
                          <i class="fas fa-trash"></i>
                        </button>
                      </div>
                    </td>
                  </tr>
                  <tr v-if="filteredItems.length === 0">
                    <td colspan="5" class="text-center">Nenhum cliente encontrado</td>
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

    const formatCpf = (cpf: string): string => {
      if (!cpf) return '';
      cpf = cpf.replace(/\D/g, '');
      return cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
    };

    const formatPhone = (phone: string): string => {
      if (!phone) return '';
      phone = phone.replace(/\D/g, '');
      if (phone.length === 11) {
        return phone.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
      }
      return phone.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
    };

    const filteredItems = computed(() => {
      if (!searchTerm.value) return items.value;
      const term = searchTerm.value.toLowerCase();
      return items.value.filter(item =>
        item.nome.toLowerCase().includes(term) ||
        item.cpf.includes(term) ||
        item.telefone.includes(term)
      );
    });

    const load = async () => {
      try {
        const { data } = await api.get('/clientes');
        items.value = data;
      } catch (error) {
        notifyError('Erro ao carregar clientes');
        console.error(error);
      }
    };

    const del = (id: number) => {
      Swal.fire({
        title: 'Excluir Cliente',
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
            await api.delete(`/clientes/${id}`);
            await load();
            notifySuccess('Cliente excluído com sucesso!');
          } catch (error) {
            notifyError('Erro ao excluir cliente');
            console.error(error);
          }
        }
      });
    };

    onMounted(load);

    return { items, searchTerm, filteredItems, del, formatCpf, formatPhone };
  }
};
</script>
