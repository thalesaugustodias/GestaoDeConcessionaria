<template>
  <section class="content-header mb-3">
    <div class="container-fluid">
      <div class="row">
        <div class="col-sm-6">
          <h1><i class="fas fa-shopping-cart mr-2"></i>Detalhes da Venda</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/vendas">Vendas</router-link></li>
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
              <h3 class="card-title">Informações da Venda</h3>
              <div class="card-tools">
                <router-link to="/vendas" class="btn btn-secondary btn-sm">
                  <i class="fas fa-arrow-left"></i> Voltar
                </router-link>
              </div>
            </div>
            <div class="card-body">
              <div class="row">
                <div class="col-md-6">
                  <div class="info-box bg-light">
                    <div class="info-box-content">
                      <h5>Dados da Venda</h5>
                      <p><strong>ID:</strong> {{ v.id }}</p>
                      <p><strong>Preço:</strong> {{ formatCurrency(v.precoVenda) }}</p>
                      <p><strong>Data:</strong> {{ formatDate(v.dataVenda) }}</p>
                      <p><strong>Protocolo:</strong> {{ v.protocoloVenda }}</p>
                    </div>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="info-box bg-light">
                    <div class="info-box-content">
                      <h5>Participantes</h5>
                      <p><strong>Veículo:</strong> {{ v.veiculoModelo }}</p>
                      <p><strong>Concessionária:</strong> {{ v.concessionariaNome }}</p>
                      <p><strong>Cliente:</strong> {{ v.clienteNome }}</p>
                    </div>
                  </div>
                </div>
              </div>

              <div class="row mt-4">
                <div class="col-md-12">
                  <div class="card card-outline card-success">
                    <div class="card-header">
                      <h3 class="card-title">Comprovante de Venda</h3>
                    </div>
                    <div class="card-body">
                      <div class="row">
                        <div class="col-md-12 text-center">
                          <button class="btn btn-success" @click="imprimirComprovante">
                            <i class="fas fa-print mr-1"></i>Imprimir Comprovante
                          </button>
                        </div>
                      </div>
                    </div>
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
import { notifyError, notifyInfo } from '@/services/notificationService';

export default {
  name: 'Detail',
  setup() {
    const route = useRoute();
    const id = route.params.id as string;
    const v = reactive<any>({});

    const formatCurrency = (valor: number): string => {
      return new Intl.NumberFormat('pt-BR', {
        style: 'currency',
        currency: 'BRL'
      }).format(valor);
    };

    const formatDate = (date: string): string => {
      if (!date) return '';
      return new Date(date).toLocaleString('pt-BR', {
        day: '2-digit',
        month: '2-digit',
        year: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
      });
    };

    const imprimirComprovante = () => {
      notifyInfo('Preparando comprovante para impressão...');
      setTimeout(() => {
        window.print();
      }, 500);
    };

    onMounted(async () => {
      try {
        const { data } = await api.get(`/vendas/${id}`);
        Object.assign(v, data);
      } catch (error) {
        notifyError('Erro ao carregar dados da venda');
        console.error(error);
      }
    });

    return { v, formatCurrency, formatDate, imprimirComprovante };
  }
};
</script>
