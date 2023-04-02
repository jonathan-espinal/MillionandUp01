<template>
  <div class="title">Shopping Cart</div>
  <div style="width: 400px">
    <div v-if="showCart">
      <q-btn
        v-if="shoppingCart.products.length > 0"
        color="primary"
        unelevated
        @click="checkoutAction()"
        >Checkout</q-btn
      >
      <hr />
      <q-chip class="container-price" square icon="attach_money"
        >TOTAL: {{ shoppingCart.totalPrice }}</q-chip
      >
      <q-card
        flat
        bordered
        v-for="(product, productIndex) in shoppingCart.products"
        v-bind:key="productIndex"
        class="q-mb-sm"
      >
        <q-card-section>
          <div v-if="errors[product.id]">
            <div
              v-for="(error, indexError) in errors[product.id]"
              v-bind:key="indexError"
              class="text-red"
            >
              - {{ error }}
            </div>
          </div>
          <div :class="{ 'text-bold': true, 'text-red': product.data.error }">
            {{ product.data.title }}
          </div>
          <q-chip
            style="margin-left: 0"
            class="container-price"
            square
            icon="attach_money"
            >{{ product.price }}</q-chip
          >
          <q-chip class="container-price" square icon="attach_money"
            >TOTAL: {{ product.totalPrice }}</q-chip
          >
          <div>
            <q-input
              type="number"
              dense
              outlined
              v-model="product.quantity"
              label="Quantity"
              class="q-mb-sm"
            />
            <q-btn-group flat style="width: 100%">
              <q-btn
                color="primary"
                style="width: 50%"
                @click="updateAction(product)"
                >Update</q-btn
              >
              <q-btn
                color="red"
                style="width: 50%"
                @click="deleteAction(product)"
                >Delete</q-btn
              >
            </q-btn-group>
          </div>
        </q-card-section>
      </q-card>
    </div>
    <div v-else>
      <q-spinner color="primary" size="3em" />
    </div>
  </div>
</template>

<script setup>
import { onMounted, ref } from "vue";
import { useQuasar } from "quasar";
import {
  addToCartAsync,
  checkoutAsync,
  retrieveAsync as retrieveCartAsync,
} from "src/services/ShoppingCartService.js";
import { retrieveAsync as retrieveProductAsync } from "src/services/ProductService.js";

const $q = useQuasar();

const shoppingCart = ref({});
const showCart = ref(false);
const errors = ref({});

window.addEventListener("LOGIN_READY", (data) => {
  retrieveAction();
});

const updateAction = async (product) => {
  const data = {
    source: product.productSource,
    id: product.productId,
    quantity: product.quantity,
  };
  try {
    await addToCartAsync(data);
    await retrieveAction();
  } catch (errors) {
    var errorString = "";
    Object.entries(errors.response.data).forEach(([key, value]) => {
      for (var error of value) {
        errorString = errorString + "- " + error + "<br />";
      }
    });

    $q.notify({
      color: "red",
      textColor: "white",
      icon: "warning",
      position: "top",
      html: true,
      message: errorString,
    });
  }
};

const deleteAction = async (product) => {
  const data = {
    source: product.productSource,
    id: product.productId,
    quantity: 0,
  };
  await addToCartAsync(data);
  await retrieveAction();
};

const retrieveAction = async () => {
  showCart.value = false;
  try {
    shoppingCart.value = await retrieveCartAsync();
    for (var product in shoppingCart.value.products) {
      try {
        var response = await retrieveProductAsync(
          shoppingCart.value.products[product].productSource,
          shoppingCart.value.products[product].productId
        );
        shoppingCart.value.products[product].data = response;
        shoppingCart.value.products[product].data.error = false;
      } catch (error) {
        shoppingCart.value.products[product].data = {
          title: "PRODUCT IS NO LONGER AVAILABLE.",
          error: true,
        };
      }
    }
  } catch (error) {
    shoppingCart.value.totalPrice = 0;
    shoppingCart.value.products = [];
  }
  showCart.value = true;
};

const checkoutAction = async () => {
  try {
    await checkoutAsync();
    $q.notify({
      color: "positive",
      textColor: "white",
      icon: "done",
      position: "top",
      html: true,
      message: "Checkout complete.",
    });
    await retrieveAction();
  } catch (error) {
    errors.value = error.response.data;
    $q.notify({
      color: "red",
      textColor: "white",
      icon: "warning",
      position: "top",
      html: true,
      message:
        "One or more problems were found with the checkout, please check the errors in the product list.",
    });
  }
};

onMounted(async () => {
  await retrieveAction();
});
</script>
