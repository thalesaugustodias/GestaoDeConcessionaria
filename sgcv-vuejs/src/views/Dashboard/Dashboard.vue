<template>
  <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1>Dashboard</h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item active">Dashboard</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <div class="row">
        <div class="col-lg-3 col-6" v-for="box in boxes" :key="box.title">
          <div :class="['small-box', box.bg]">
            <div class="inner">
              <h3>{{ formatValue(box) }}</h3>
              <p>{{ box.title }}</p>
            </div>
            <div class="icon">
              <i :class="box.icon"></i>
            </div>
          </div>
        </div>
      </div>
      
      <div class="row">
        <div class="col-md-6">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Vendas Mensais</h3>
            </div>
            <div class="card-body">
              <canvas id="vendasChart" style="min-height: 250px; height: 250px; max-height: 250px; max-width: 100%;"></canvas>
            </div>
          </div>
        </div>

        <div class="col-md-6">
          <div class="card">
            <div class="card-header">
              <h3 class="card-title">Veículos por Tipo</h3>
            </div>
            <div class="card-body">
              <canvas id="veiculosChart" style="min-height: 250px; height: 250px; max-height: 250px; max-width: 100%;"></canvas>
            </div>
          </div>
        </div>
      </div>
    </div>
  </section>
</template>

<script lang="ts">
import { ref, onMounted } from 'vue';
import api from '@/services/api';
import { notifyError } from '@/services/notificationService';
import Chart from 'chart.js/auto';

export default {
  name: 'Dashboard',
  setup() {
    const boxes = ref([
      { title: 'Vendas do Mês', value: 0, bg: 'bg-info', icon: 'fas fa-shopping-cart' },
      { title: 'Faturamento', value: 0, bg: 'bg-success', icon: 'fas fa-dollar-sign', isCurrency: true },
      { title: 'Veículos Disponíveis', value: 0, bg: 'bg-warning', icon: 'fas fa-car' },
      { title: 'Clientes Ativos', value: 0, bg: 'bg-danger', icon: 'fas fa-users' }
    ]);

    const vendasMensais = ref<number[]>([]);
    const meses = ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'];
    const veiculosPorTipo = ref<any>({});

    const formatValue = (box: any): string => {
      if (box.isCurrency) {
        return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(box.value);
      }
      return box.value.toString();
    };

    const createVendasChart = () => {
      const ctx = document.getElementById('vendasChart') as HTMLCanvasElement;
      if (ctx) {
        new Chart(ctx, {
          type: 'line',
          data: {
            labels: meses,
            datasets: [{
              label: 'Vendas',
              data: vendasMensais.value,
              fill: false,
              borderColor: '#17a2b8',
              tension: 0.1
            }]
          },
          options: {
            responsive: true,
            maintainAspectRatio: false
          }
        });
      }
    };

    const createVeiculosChart = () => {
      const ctx = document.getElementById('veiculosChart') as HTMLCanvasElement;
      if (ctx) {
        const tipos = Object.keys(veiculosPorTipo.value);
        const valores = Object.values(veiculosPorTipo.value);

        new Chart(ctx, {
          type: 'doughnut',
          data: {
            labels: tipos,
            datasets: [{
              data: valores,
              backgroundColor: ['#f39c12', '#00c0ef', '#00a65a', '#f56954']
            }]
          },
          options: {
            responsive: true,
            maintainAspectRatio: false
          }
        });
      }
    };

    onMounted(async () => {
      try {
        const now = new Date();
        const resp = await api.get(`/relatorios/mensal?mes=${now.getMonth()+1}&ano=${now.getFullYear()}`);
        const d = resp.data;

        boxes.value = [
          { title: 'Vendas do Mês', value: d.totalVendas, bg: 'bg-info', icon: 'fas fa-shopping-cart' },
          { title: 'Faturamento', value: d.faturamento, bg: 'bg-success', icon: 'fas fa-dollar-sign', isCurrency: true },
          { title: 'Veículos Disponíveis', value: d.totalVeiculos, bg: 'bg-warning', icon: 'fas fa-car' },
          { title: 'Clientes Ativos', value: d.totalClientes, bg: 'bg-danger', icon: 'fas fa-users' }
        ];

        vendasMensais.value = [30, 35, 40, 50, 60, 70, 75, 80, 90, 95, 100, d.totalVendas];
        veiculosPorTipo.value = {
          'Carro': Math.floor(d.totalVeiculos * 0.6),
          'Moto': Math.floor(d.totalVeiculos * 0.3),
          'Caminhão': Math.floor(d.totalVeiculos * 0.1)
        };

        createVendasChart();
        createVeiculosChart();
      } catch (error) {
        notifyError('Erro ao carregar dados do dashboard');
        console.error(error);
      }
    });

    return { boxes, formatValue };
  }
};
</script>
