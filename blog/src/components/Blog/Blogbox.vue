<script setup lang="ts">
import {Blog} from "./class/Blog.ts";

defineProps({
  info:Blog
})
</script>

<template>
  <div class="card">
    <div class="right">
      <div><span>{{info?.title}}</span></div>
      <div><span>{{info?.content}}</span></div>
    </div>
  </div>
</template>

<style scoped>

</style>
