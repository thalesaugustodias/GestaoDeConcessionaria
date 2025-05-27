import { ref } from 'vue'
import type { Ref } from 'vue' 

interface ValidationErrors {
  [key: string]: string[];
}

export function useServerValidation() {
  const errors: Ref<ValidationErrors> = ref({});

  const clearErrors = () => {
    errors.value = {};
  };

  const handleError = (error: any) => {
    if (error.response && error.response.status === 422) {
      errors.value = error.response.data.errors || {};
    } else {
      console.error('Erro na validação:', error);
    }
  };

  return { errors, clearErrors, handleError };
}
