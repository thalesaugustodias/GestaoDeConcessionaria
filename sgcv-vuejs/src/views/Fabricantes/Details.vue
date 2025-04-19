<template>
  <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1><i class="fas fa-industry mr-2"></i>Detalhes do Fabricante</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/fabricantes">Fabricantes</router-link></li>
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
              <h3 class="card-title">Informações do Fabricante</h3>
              <div class="card-tools">
                <router-link to="/fabricantes" class="btn btn-secondary btn-sm">
                  <i class="fas fa-arrow-left"></i> Voltar
                </router-link>
              </div>
            </div>
            <div class="card-body">
              <div class="row">
                <div class="col-md-12">
                  <div class="info-box bg-light">
                    <div class="info-box-content">
                      <p><strong>ID:</strong> {{ model.id }}</p>
                      <p><strong>Nome:</strong> {{ model.nome }}</p>
                      <p><strong>País de Origem:</strong> {{ model.paisOrigem }}</p>
                      <p><strong>Ano de Fundação:</strong> {{ model.anoFundacao }}</p>
                      <p><strong>Website:</strong> <a :href="model.website" target="_blank">{{ model.website }}</a></p>
                    </div>
                  </div>

                  <div class="mt-4">
                    <router-link :to="`/fabricantes/${model.id}/edit`" class="btn btn-warning">
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
    const model = reactive({ id: 0, nome: '', paisOrigem: '', anoFundacao: 0, website: '' });

    onMounted(async () => {
      try {
        const resp = await api.get(`/fabricantes/${id}`);
        Object.assign(model, resp.data);
      } catch (error) {
        notifyError('Erro ao carregar dados do fabricante');
        console.error(error);
      }
    });

    return { model };
  }
};
</script>
