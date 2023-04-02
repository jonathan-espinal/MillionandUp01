<template>
  <q-layout view="lHh Lpr lFf">
    <q-header>
      <q-toolbar>
        <div><q-btn to="/" flat label="Home" /></div>
        <div><q-btn to="/shoppingCart" flat label="View Cart" /></div>
        <q-separator vertical class="q-ml-md q-mr-md" />
        <div><q-btn flat label="Login" @click="showLoginAction()" /></div>
        <template v-if="login.username != null">
          <q-avatar class="q-ml-md" color="secondary" text-color="white">{{
            login.username[0].toUpperCase()
          }}</q-avatar>
          <div class="q-ml-md">{{ login.username }}</div>
        </template>
        <q-space />
        <img style="height: 50px; margin: 5px" src="DeluxeStoreLogo.png" />
      </q-toolbar>
    </q-header>
    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>

  <q-dialog v-model="showLogin">
    <q-card flat bordered>
      <q-card-section>Use your username or create a new one.</q-card-section>
      <q-card-section>
        <q-input label="Username" dense outlined v-model="login.username" />
      </q-card-section>
      <q-card-actions align="right">
        <q-btn
          :loading="loading"
          :disable="loading"
          color="primary"
          unelevated
          @click="loginAction()"
          >Login</q-btn
        >
      </q-card-actions>
    </q-card>
  </q-dialog>
</template>

<script setup>
import { reactive, onMounted, ref } from "vue";
import { SessionStorage } from "quasar";
import { authAsync } from "src/services/AuthService.js";
import { api } from "src/boot/axios";
import jwt_decode from "jwt-decode";

const showLogin = ref(false);
const loading = ref(false);

const login = reactive({
  username: null,
});

onMounted(() => {
  const token = SessionStorage.getItem("Token");
  if (token != null) {
    var decoded = jwt_decode(token);
    console.log(decoded);
    login.username = decoded.Username;
  }
});

window.addEventListener("SHOW_LOGIN", (data) => {
  showLogin.value = true;
});

const showLoginAction = () => {
  showLogin.value = true;
};

const loginAction = async () => {
  loading.value = true;
  try {
    if (login.username == null || login.username == "") return;
    const response = await authAsync(login);
    SessionStorage.set("Token", response.token);
    api.defaults.headers.common["Authorization"] = "Bearer " + response.token;
    api.defaults.headers["Authorization"] = "Bearer " + response.token;
    var decoded = jwt_decode(response.token);
    login.username = decoded.Username;
    showLogin.value = false;
    window.dispatchEvent(
      new CustomEvent("LOGIN_READY", {
        detail: {
          success: true,
        },
      })
    );
  } catch (error) {
    console.log(error);
  }
  loading.value = false;
};
</script>
