import api from './api';

export async function buscarCep(cep: string): Promise<CepResponse> {
  const cepLimpo = cep.replace(/\D/g, '');
  
  if (cepLimpo.length !== 8) {
    throw new Error('CEP deve conter 8 dígitos');
  }
  
  const response = await api.get<CepResponse>(`/cep/${cepLimpo}`);
  return response.data;
};
