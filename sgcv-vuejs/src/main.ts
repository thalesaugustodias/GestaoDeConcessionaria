import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import validateCpf from './directives/validateCpf'

const app = createApp(App)

app.directive('validate-cpf', validateCpf)
app.directive('phone', {
  mounted(el) {
    const formatPhone = (value: string) => {
      if (!value) return ''
      value = value.replace(/\D/g, '')
      if (value.length > 11) {
        value = value.substring(0, 11)
      }
      if (value.length > 10) {
        value = value.replace(/^(\d{2})(\d{5})(\d{4}).*/, '($1) $2-$3')
      } else if (value.length > 6) {
        value = value.replace(/^(\d{2})(\d{4})(\d{0,4}).*/, '($1) $2-$3')
      } else if (value.length > 2) {
        value = value.replace(/^(\d{2})(\d{0,5})/, '($1) $2')
      } else if (value.length > 0) {
        value = value.replace(/^(\d*)/, '($1')
      }
      return value
    }

    const inputHandler = () => {
      el.value = formatPhone(el.value)
    }

    el.addEventListener('input', inputHandler)
  }
})

app.use(router)
app.mount('#app')
