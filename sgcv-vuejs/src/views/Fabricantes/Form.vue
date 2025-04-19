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
import { reactive, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import api from '@/services/api'
import { useServerValidation } from '@/composables/useServerValidation'
import { notifySuccess, notifyError } from '@/services/notificationService'

export default {
  name: 'Form',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const id = route.params.id as string | undefined
    const isEditing = Boolean(id)
    const model = reactive({
      nome: '',
      paisOrigem: '',
      anoFundacao: new Date().getFullYear(),
      website: ''
    })

    const { errors, clearErrors, handleError } = useServerValidation()

    onMounted(async () => {
      if (isEditing && id) {
        clearErrors()
        try {
          const resp = await api.get(`/fabricantes/${id}`)
          Object.assign(model, resp.data)
        } catch (err) {
          notifyError('Erro ao carregar dados do fabricante')
          handleError(err)
        }
      }
    })

    const submit = async () => {
      clearErrors()
      try {
        if (isEditing && id) {
          await api.put(`/fabricantes/${id}`, model)
          notifySuccess('Fabricante atualizado com sucesso!')
        } else {
          await api.post('/fabricantes', model)
          notifySuccess('Fabricante cadastrado com sucesso!')
        }
        router.push('/fabricantes')
      } catch (err) {
        notifyError('Erro ao salvar fabricante')
        handleError(err)
      }
    }

    return { model, isEditing, submit, errors }
  }
}
</script>
