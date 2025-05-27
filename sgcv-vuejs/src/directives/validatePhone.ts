import { DirectiveBinding } from 'vue';

export default {
  mounted(el: HTMLInputElement, binding: DirectiveBinding) {
    const formatPhone = (value: string): string => {
      if (!value) return '';
      value = value.replace(/\D/g, '');
      if (value.length > 11) {
        value = value.substring(0, 11);
      }
      if (value.length > 10) {
        value = value.replace(/^(\d{2})(\d{5})(\d{4}).*/, '($1) $2-$3');
      } else if (value.length > 6) {
        value = value.replace(/^(\d{2})(\d{4})(\d{0,4}).*/, '($1) $2-$3');
      } else if (value.length > 2) {
        value = value.replace(/^(\d{2})(\d{0,5})/, '($1) $2');
      } else if (value.length > 0) {
        value = value.replace(/^(\d*)/, '($1');
      }
      return value;
    };

    const validatePhone = (phone: string): boolean => {
      const phoneClean = phone.replace(/\D/g, '');
      return phoneClean.length === 10 || phoneClean.length === 11;
    };

    const inputHandler = (event: Event) => {
      const input = event.target as HTMLInputElement;
      input.value = formatPhone(input.value);
      
      if (input.value.length > 0) {
        if (validatePhone(input.value)) {
          input.classList.remove('is-invalid');
          input.classList.add('is-valid');
        } else {
          input.classList.remove('is-valid');
          input.classList.add('is-invalid');
        }
      } else {
        input.classList.remove('is-valid', 'is-invalid');
      }
    };

    el.addEventListener('input', inputHandler);
    el.addEventListener('blur', (event) => {
      const input = event.target as HTMLInputElement;
      if (input.value.length > 0 && !validatePhone(input.value)) {
        let errorEl = el.nextElementSibling;
        if (!errorEl || !errorEl.classList.contains('invalid-feedback')) {
          errorEl = document.createElement('div');
          errorEl.className = 'invalid-feedback';
          el.parentNode?.insertBefore(errorEl, el.nextSibling);
        }
        errorEl.textContent = 'Telefone inválido';
        errorEl.style.display = 'block';
      }
    });
  }
};
