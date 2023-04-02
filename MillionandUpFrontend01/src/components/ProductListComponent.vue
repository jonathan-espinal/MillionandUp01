<template>
  <div class="title">Products</div>
  <div class="container-filters">
    <q-select
      outlined
      dense
      v-model="filters.order"
      emit-value
      map-options
      :options="[
        { label: 'Title', value: 0 },
        { label: 'Price', value: 1 },
      ]"
      label="Order"
      class="filter-order"
      @update:model-value="filterAction()"
    />

    <q-separator vertical class="filter-separator" />

    <q-input
      v-model="filters.filterTitle"
      label="Title"
      dense
      outlined
      class="filter-title"
    />

    <q-input
      v-model="filters.filterPriceMin"
      type="number"
      label="Price Min"
      dense
      outlined
      class="filter-price-min"
    />
    <q-input
      v-model="filters.filterPriceMax"
      type="number"
      label="Price Max"
      dense
      outlined
      class="filter-price-max"
    />

    <q-btn-group unelevated>
      <q-btn color="primary" unelevated @click="filterAction()">Search</q-btn>
      <q-btn color="secondary" unelevated @click="clearFilterAction()">
        Claer</q-btn
      >
    </q-btn-group>
  </div>
  <div v-if="loading">
    <q-spinner color="primary" size="3em" />
  </div>
  <div v-else>
    <div class="container">
      <template
        v-for="(product, indexProduct) in products"
        v-bind:key="indexProduct"
      >
        <q-card flat dark class="product-card">
          <q-img
            :src="product.images[0]"
            spinner-color="white"
            style="height: 285px"
          />

          <q-card-section>
            <div class="text-bold container-title">{{ product.title }}</div>
            <q-chip class="container-price" square icon="attach_money">{{
              product.price
            }}</q-chip>
          </q-card-section>

          <q-separator dark />

          <q-card-actions vertical align="right">
            <q-btn color="primary" unelevated @click="showDetailAction(product)"
              >View detail</q-btn
            >
          </q-card-actions>
        </q-card>
      </template>
    </div>
    <q-dialog full-width v-model="showDetail">
      <product-detail-component :product="currentProduct" />
    </q-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from "vue";
import { listAsync } from "src/services/ProductService.js";

import ProductDetailComponent from "src/components/ProductDetailComponent.vue";

window.addEventListener("ADD_TO_CART", (data) => {
  showDetail.value = false;
});

const products = ref([]);
const loading = ref(true);
const showDetail = ref(false);
const currentProduct = ref({});

const filters = reactive({
  order: 0,
  filterTitle: null,
  filterPriceMin: 0,
  filterPriceMax: 0,
});

onMounted(async () => {
  products.value = await listAsync(filters);
  loading.value = false;
});

const showDetailAction = (product) => {
  showDetail.value = true;
  currentProduct.value = product;
};

const filterAction = async () => {
  loading.value = true;
  products.value = await listAsync(filters);
  loading.value = false;
};

const clearFilterAction = async () => {
  filters.filterTitle = null;
  filters.filterPriceMax = 0;
  filters.filterPriceMin = 0;
  filterAction();
};
</script>

<style scoped lang="sass">
.container
  display: flex
  flex-direction: row
  flex-wrap: wrap
  justify-content: start

.container-description
  overflow-y: auto
  height: 150px

.container-title
  height: 16px
  white-space: nowrap
  overflow: hidden
  text-overflow: ellipsis

.product-card
  width: calc(16.66% - 15px)
  max-height: 425px
  min-height: 425px
  margin-bottom: 15px
  margin-right: 15px
  background: radial-gradient(circle, $primary 0%, $grey-10 100%)
  border: 2px solid $grey-10

.product-card:hover
  border: 2px solid $primary

.container-filters
  display: flex
  margin-bottom: 15px
  width: 100wv

.filter-order, .filter-separator, .filter-title, .filter-price-max
  margin-right: 12px

.container-price
  margin-left: 0
</style>
