<template>
  <q-page padding>
    <product-list-component />
  </q-page>
</template>

<script>
import { defineComponent } from "vue";
import ProductListComponent from "src/components/ProductListComponent.vue";

export default defineComponent({
  name: "IndexPage",

  components: {
    ProductListComponent,
  },
});
</script>
