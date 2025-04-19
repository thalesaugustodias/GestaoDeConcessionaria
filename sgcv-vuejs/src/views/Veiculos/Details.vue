<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1><i class="fas fa-car mr-2"></i>Detalhes do Veículo</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/veiculos">Veículos</router-link></li>
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
              <h3 class="card-title">Informações do Veículo</h3>
              <div class="card-tools">
                <router-link to="/veiculos" class="btn btn-secondary btn-sm">
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
                      <p><strong>Modelo:</strong> {{ m.modelo }}</p>
                      <p><strong>Ano:</strong> {{ m.anoFabricacao }}</p>
                      <p><strong>Preço:</strong> {{ formatCurrency(m.preco) }}</p>
                      <p><strong>Tipo:</strong> {{ m.tipo }}</p>
                      <p><strong>Fabricante:</strong> {{ m.nomeFabricante || 'Não informado'}}</p>
                      <p><strong>Descrição:</strong> {{ m.descricao || 'Não informada' }}</p>
                    </div>
                  </div>

                  <div class="mt-4">
                    <router-link :to="`/veiculos/${m.id}/edit`" class="btn btn-warning">
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

    const formatCurrency = (valor: number): string => {
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
      }).format(valor);
    };

    onMounted(async () => {
      try {
        const { data } = await api.get(`/veiculos/${id}`);
        Object.assign(m, data);
      } catch (error) {
        notifyError('Erro ao carregar dados do veículo');
        console.error(error);
      }
    });

    return { m, formatCurrency };
  }
};
</script>
