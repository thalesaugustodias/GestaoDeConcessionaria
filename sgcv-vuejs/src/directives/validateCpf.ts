// validateCpf.ts
import type { DirectiveBinding } from 'vue'   // <-- import só para tipo

export default {
  mounted(el: HTMLInputElement, binding: DirectiveBinding) {
    const formatCpf = (cpf: string): string => {
      cpf = cpf.replace(/\D/g, '')
      if (cpf.length <= 11) {
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2')
        cpf = cpf.replace(/(\d{3})(\d)/, '$1.$2')
        cpf = cpf.replace(/(\d{3})(\d{1,2})$/, '$1-$2')
      }
      return cpf
    }

    const inputHandler = (event: Event) => {
      const input = event.target as HTMLInputElement
      input.value = formatCpf(input.value)
    }

    el.addEventListener('input', inputHandler)
    el.addEventListener('blur', (event) => {
      const input = event.target as HTMLInputElement
      const cpf = input.value.replace(/\D/g, '')

      if (cpf.length > 0 && cpf.length !== 11) {
        input.classList.add('is-invalid')

        let errorEl = el.nextElementSibling
        if (!errorEl || !errorEl.classList.contains('invalid-feedback')) {
          errorEl = document.createElement('div')
          errorEl.className = 'invalid-feedback'
          el.parentNode?.insertBefore(errorEl, el.nextSibling)
        }
        errorEl.textContent = 'CPF deve conter 11 dígitos'
        errorEl.style.display = 'block'

        notifyWarning('CPF deve conter 11 dígitos')
      }
    })
  }
}
