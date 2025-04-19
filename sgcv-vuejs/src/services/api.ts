import axios from 'axios';
import { notifyError } from './notificationService';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_URL || 'https://localhost:7270/api',
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  }
});

api.interceptors.request.use(
  config => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  error => {
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    if (error.response && error.response.status === 401) {
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      
      notifyError('Sessão expirada. Por favor, faça login novamente.');
      
      window.location.href = '/login';
    }
    
    if (error.response && error.response.data && error.response.data.message) {
      notifyError(error.response.data.message);
    } else if (error.message) {
      notifyError(error.message);
    } else {
      notifyError('Ocorreu um erro na requisição');
    }
    
    return Promise.reject(error);
  }
);

export default api;
