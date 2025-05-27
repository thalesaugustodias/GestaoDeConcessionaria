<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1><i class="fas fa-user mr-2"></i>Detalhes do Cliente</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/clientes">Clientes</router-link></li>
            <li class="breadcrumb-item active">Detalhes</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <div class="row">
        <div class="col-md-12">
          <div class="card card-primary card-outline">
            <div class="card-header">
              <h3 class="card-title">Informações do Cliente</h3>
              <div class="card-tools">
                <router-link to="/clientes" class="btn btn-secondary btn-sm">
                  <i class="fas fa-arrow-left"></i> Voltar
                </router-link>
              </div>
            </div>
            <div class="card-body">
              <div class="row">
                <div class="col-md-12">
                  <div class="info-box bg-light">
                    <div class="info-box-content">
                      <p><strong>ID:</strong> {{ m.id }}</p>
                      <p><strong>Nome:</strong> {{ m.nome }}</p>
                      <p><strong>CPF:</strong> {{ formatCpf(m.cpf) }}</p>
                      <p><strong>Telefone:</strong> {{ formatPhone(m.telefone) }}</p>
                    </div>
                  </div>

                  <div class="mt-4">
                    <router-link :to="`/clientes/${m.id}/edit`" class="btn btn-warning">
                      <i class="fas fa-edit"></i> Editar
                    </router-link>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script lang="ts">
import { reactive, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import api from '@/services/api';
import { notifyError } from '@/services/notificationService';

export default {
  name: 'Detail',
  setup() {
    const route = useRoute();
    const id = route.params.id as string;
    const m = reactive<any>({});

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

    onMounted(async () => {
      try {
        const { data } = await api.get(`/clientes/${id}`);
        Object.assign(m, data);
      } catch (error) {
        notifyError('Erro ao carregar dados do cliente');
        console.error(error);
      }
    });

    return { m, formatCpf, formatPhone };
  }
};
</script>
