<template>
  <section class="content-header">
    <div class="container-fluid">
      <div class="row mb-2">
        <div class="col-sm-6">
          <h1>
            <i :class="isEditing ? 'fas fa-edit' : 'fas fa-plus-circle'"></i>
            {{ isEditing ? 'Editar' : 'Cadastrar' }} Fabricante
          </h1>
        </div>
        <div class="col-sm-6">
          <ol class="breadcrumb float-sm-right">
            <li class="breadcrumb-item"><router-link to="/dashboard">Home</router-link></li>
            <li class="breadcrumb-item"><router-link to="/fabricantes">Fabricantes</router-link></li>
            <li class="breadcrumb-item active">{{ isEditing ? 'Editar' : 'Cadastrar' }}</li>
          </ol>
        </div>
      </div>
    </div>
  </section>

  <section class="content">
    <div class="container-fluid">
      <form @submit.prevent="submit">
        <div class="card card-primary">
          <div class="card-header">
            <h3 class="card-title">Dados do Fabricante</h3>
            <div class="card-tools">
              <router-link to="/fabricantes" class="btn btn-secondary btn-sm">
                <i class="fas fa-arrow-left"></i> Voltar
              </router-link>
            </div>
          </div>
          <div class="card-body">
            <div class="form-group">
              <label>Nome</label>
              <input v-model="model.nome" class="form-control" :class="{'is-invalid': errors.nome}" />
              <div v-if="errors.nome" class="invalid-feedback">{{ errors.nome[0] }}</div>
            </div>

            <div class="form-group">
              <label>País de Origem</label>
              <input v-model="model.paisOrigem" class="form-control" :class="{'is-invalid': errors.paisOrigem}" />
              <div v-if="errors.paisOrigem" class="invalid-feedback">{{ errors.paisOrigem[0] }}</div>
            </div>

            <div class="form-group">
              <label>Ano de Fundação</label>
              <input v-model.number="model.anoFundacao"
                     type="number"
                     min="1800"
                     max="9999"
                     class="form-control"
                     :class="{'is-invalid': errors.anoFundacao}" />
              <div v-if="errors.anoFundacao" class="invalid-feedback">{{ errors.anoFundacao[0] }}</div>
            </div>

            <div class="form-group">
              <label>Website</label>
              <input v-model="model.website"
                     type="url"
                     class="form-control"
                     :class="{'is-invalid': errors.website}"
                     placeholder="https://www.exemplo.com" />
              <div v-if="errors.website" class="invalid-feedback">{{ errors.website[0] }}</div>
            </div>
          </div>
          <div class="card-footer">
            <button type="submit" class="btn btn-success float-right">
              <i class="fas fa-save"></i>
              {{ isEditing ? 'Atualizar' : 'Salvar' }}
            </button>
          </div>
        </div>
      </form>
    </div>
  </section>
</template>

<script lang="ts">
  import { reactive, ref, onMounted } from 'vue'
  import { useRoute, useRouter } from 'vue-router'
  import api from '@/services/api'
  import { useServerValidation } from '@/composables/useServerValidation'
  import { notifySuccess, notifyError, notifyWarning } from '@/services/notificationService'

  export default {
    name: 'FabricanteForm',
    setup() {
      const route = useRoute()
      const router = useRouter()
      const id = route.params.id as string | undefined
      const isEditing = Boolean(id)
      const loading = ref(false)

      const model = reactive({
        nome: '',
        paisOrigem: '',
        anoFundacao: new Date().getFullYear(),
        website: ''
      })

      const { errors, clearErrors, handleError } = useServerValidation()

      const validateForm = (): boolean => {
        if (!model.nome || model.nome.trim() === '') {
          notifyWarning('O nome do fabricante é obrigatório')
          return false
        }

        if (!model.paisOrigem || model.paisOrigem.trim() === '') {
          notifyWarning('O país de origem é obrigatório')
          return false
        }

        if (!model.anoFundacao || model.anoFundacao < 1800 || model.anoFundacao > 9999) {
          notifyWarning('O ano de fundação deve estar entre 1800 e 9999')
          return false
        }

        if (model.website && !model.website.match(/^https?:\/\/.+\..+/)) {
          notifyWarning('O website deve ser uma URL válida (ex: https://www.exemplo.com)')
          return false
        }

        return true
      }

      onMounted(async () => {
        if (isEditing && id) {
          loading.value = true
          clearErrors()
          try {
            const resp = await api.get(`/fabricantes/${id}`)
            Object.assign(model, resp.data)
          } catch (err: any) {

            if (err.response && err.response.data && err.response.data.message) {
              notifyError(err.response.data.message)
            } else {
              notifyError('Erro ao carregar dados do fabricante')
            }
            handleError(err)
          } finally {
            loading.value = false
          }
        }
      })

      const submit = async () => {
        clearErrors()

        if (!validateForm()) {
          return
        }

        loading.value = true

        try {
          if (isEditing && id) {
            await api.put(`/fabricantes/${id}`, model)
            notifySuccess('Fabricante atualizado com sucesso!')
          } else {
            await api.post('/fabricantes', model)
            notifySuccess('Fabricante cadastrado com sucesso!')
          }
          router.push('/fabricantes')
        } catch (err: any) {

          if (err.response && err.response.data && err.response.data.message) {
            notifyError(err.response.data.message)
          } else if (err.response && err.response.status === 400) {
            notifyError('Dados inválidos. Verifique os campos e tente novamente.')
          } else {
            notifyError('Erro ao salvar fabricante. Verifique sua conexão e tente novamente.')
          }
          handleError(err)
        } finally {
          loading.value = false
        }
      }

      return { model, isEditing, submit, errors, loading }
    }
  }
</script>
