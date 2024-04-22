<script setup lang="ts">

</script>

<template>
  <div id="bigbox">
    <div id="loginbox" style="background-color: #d1edc4">
      <router-view></router-view>
    </div>
  </div>
</template>

<style scoped>
#loginbox{
  position: absolute;
  width: 100%;
  height: 100%;
}

</style>
