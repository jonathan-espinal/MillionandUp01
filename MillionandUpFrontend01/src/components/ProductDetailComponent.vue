<template>
  <q-card flat bordered style="width: 80vw">
    <q-bar class="bg-primary text-white">
      <div>Product Detail</div>

      <q-space />

      <q-btn dense flat icon="close" v-close-popup>
        <q-tooltip>Close</q-tooltip>
      </q-btn>
    </q-bar>
    <q-card-section horizontal>
      <q-card-section style="width: 50%">
        <div style="display: flex">
          <div style="width: 50%">
            <q-img
              :src="currentImage"
              spinner-color="white"
              :ratio="1"
              fit="contain"
              style="height: 500px; border: 2px solid #000"
            />
          </div>
          <div style="width: 50%; height: 500px; overflow-y: scroll">
            <q-btn
              v-for="(image, indexImage) in product.images"
              v-bind:key="indexImage"
              flat
              style="margin: 0 15px 15px 15px"
              @click="currentImage = image"
            >
              <q-img
                :src="image"
                spinner-color="white"
                style="height: 150px; width: 150px"
              />
            </q-btn>
          </div>
        </div>
      </q-card-section>

      <q-separator vertical />

      <q-card-section style="width: 50%">
        <q-card-section>
          <div>
            <div class="text-bold container-title">{{ product.title }}</div>
            <q-chip class="container-price" square icon="attach_money">{{
              product.price
            }}</q-chip>
            <q-chip
              :color="product.quantityAvailable == 0 ? 'red' : 'gray-4'"
              class="container-price"
              square
              icon="inventory"
              >{{ product.quantityAvailable }}</q-chip
            >
          </div>
          <div>
            <q-rating
              v-model="rating"
              icon="star_border"
              icon-selected="star"
              icon-half="star_half"
              size="2em"
              color="primary"
              readonly
            />
          </div>
        </q-card-section>
        <q-card-section class="container-description">
          {{ product.description }}
        </q-card-section>
      </q-card-section>
    </q-card-section>
    <q-card-actions align="right">
      <q-input
        type="number"
        dense
        outlined
        v-model="data.quantity"
        label="Quantity"
        class="q-mr-md"
      />
      <q-btn
        color="primary"
        :loading="loading"
        :disable="loading"
        unelevated
        @click="addToCartAction()"
        >Add To Cart</q-btn
      >
    </q-card-actions>
  </q-card>
</template>

<script setup>
import { defineProps, reactive, ref } from "vue";
import { SessionStorage, useQuasar } from "quasar";
import { addToCartAsync } from "src/services/ShoppingCartService.js";

const $q = useQuasar();

const loading = ref(false);
const props = defineProps({ product: {} });
const rating = ref(props.product.rating);
const currentImage = ref(props.product.images[0]);

const data = reactive({
  source: props.product.source,
  id: props.product.id,
  quantity: 0,
});

window.addEventListener("LOGIN_READY", (data) => {
  // addToCartAction();
});

const addToCartAction = async () => {
  const token = SessionStorage.getItem("Token");
  if (token == null) {
    window.dispatchEvent(
      new CustomEvent("SHOW_LOGIN", {
        detail: {
          show: true,
        },
      })
    );
  } else {
    loading.value = true;
    try {
      const response = await addToCartAsync(data);

      window.dispatchEvent(
        new CustomEvent("ADD_TO_CART", {
          detail: response,
        })
      );

      $q.notify({
        color: "positive",
        textColor: "white",
        icon: "done",
        position: "top",
        html: true,
        message: "The product was added to the shopping cart.",
      });
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
    loading.value = false;
  }
};
</script>
