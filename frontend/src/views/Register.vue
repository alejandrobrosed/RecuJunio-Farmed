<template>
  <div class="register-container">
    <div class="register-card">
      <div class="logo-container">
        <h1 class="logo">FarMed</h1>
        <div class="logo-underline"></div>
        <p class="slogan">Tu farmacia de confianza</p>
      </div>
      
      <h2 class="form-title">Crear Cuenta</h2>
      
      <form @submit.prevent="handleRegister" class="register-form">
        <div class="form-group">
          <label for="name">Nombre completo</label>
          <input 
            type="text" 
            id="name" 
            v-model="formData.nombre" 
            required 
            placeholder="Tu nombre y apellidos"
          />
        </div>
        
        <div class="form-group">
          <label for="email">Correo electrónico</label>
          <input 
            type="email" 
            id="email" 
            v-model="formData.correo" 
            required 
            placeholder="correo@ejemplo.com"
          />
        </div>
                
        <div class="form-group">
          <label for="password">Contraseña</label>
          <input 
            :type="showPassword ? 'text' : 'password'" 
            id="password" 
            v-model="formData.password" 
            required 
            placeholder="Crea una contraseña"
          />
          <p class="password-hint">La contraseña debe tener al menos 6 caracteres</p>
        </div>
        
        <div class="form-group">
          <label for="confirmPassword">Confirmar contraseña</label>
          <input 
            :type="showConfirmPassword ? 'text' : 'password'" 
            id="confirmPassword" 
            v-model="formData.confirmPassword" 
            required 
            placeholder="Repite tu contraseña"
          />
        </div>
        
        <div class="terms-container">
          <input type="checkbox" id="terms" v-model="formData.termsAccepted" required />
          <label for="terms">
            Acepto los <a href="#" class="terms-link">términos y condiciones</a> y la <a href="#" class="terms-link">política de privacidad</a>
          </label>
        </div>
        
        <button type="submit" class="register-button">
          Crear Cuenta
        </button>
      </form>
      
      <div class="divider"></div>
      
      <div class="login-link-container">
        <p>¿Ya tienes una cuenta?</p>
        <router-link to="/login" class="login-link">Inicia sesión aquí</router-link>
      </div>
    </div>
  </div>
</template>

<script>

import axios from 'axios';
export default {
  name: 'Register',
  data() {
    return {
      formData: {
        nombre: '',
        correo: '',
        password: '',
        confirmPassword: '',
        termsAccepted: false
      },
      showPassword: false,
      showConfirmPassword: false,
      errorMessage: ''
    };
  },
  methods: {
    validateForm() {
      if (this.formData.password.length < 6) {
        alert('La contraseña debe tener al menos 6 caracteres.');
        return false;
      }
      
      if (this.formData.password !== this.formData.confirmPassword) {
        alert('Las contraseñas no coinciden.');
        return false;
      }
      
      if (!this.formData.termsAccepted) {
        alert('Debes aceptar los términos y condiciones.');
        return false;
      }
      
      return true;
    },
    async handleRegister() {
      if (!this.validateForm()) {
        return;
      }
      console.log(this.formData);
      console.log("Nombre usuario" + this.formData.nombre);
      try{
        await axios.post('http://localhost:5000/api/auth/register', {
          nombre: this.formData.nombre,
          correo: this.formData.correo,
          password: this.formData.password,
          rol: "usuario",
          activo: true
        }, { withCredentials: true});
        console.log('Registro exitoso:', this.formData);
        alert('Cuenta creada con éxito. Por favor, inicia sesión.');
        
        this.$router.push('/login');


      }catch(error){
         if(error.response)
          {
            console.error("respuesta de error", error.response);
            alert(error.response.status == 401 ? "Credenciales incorrectas" : `Error del servidor ${error.response.status}`);
          }else{
            console.error("Error desconocido", error);
            alert("Credenciales incorrectas");
          }
      }
    }
  }
};
</script>

<style lang="scss">
@import '@/assets/styles/pages/register.scss';
</style>