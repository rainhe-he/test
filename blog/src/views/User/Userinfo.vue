<script setup lang="ts">
import {reactive} from 'vue';
import {EditUserInfo} from "../../axios/http.ts";
import {ElMessage} from "element-plus";

const form = reactive({
  name:localStorage["Name"],
})

const onsubmit=async ()=>{
  let res = (await EditUserInfo(form.name)) as any
  ElMessage.success(res.data)
  localStorage["Name"] = form.name
}

</script>

<template>
  <el-row>
    <el-col :span="6">
      <el-form :model="form" label-width="120px">
        <el-form-item label="昵称">
          <el-input v-model="form.name"></el-input>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click = "onsubmit">修改</el-button>
        </el-form-item>
      </el-form>
    </el-col>
  </el-row>
</template>

<style scoped>

</style>

