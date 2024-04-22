<script setup lang="ts">
import {reactive, ref} from "vue";
import {ElMessage, FormInstance, FormRules} from "element-plus";
import router from "../../router";
import {Regist} from "../../axios/http.ts";


const ruleForm = reactive({
  name:"",
  username:"",
  userpwd:""
})
const rules = reactive<FormRules>({
  username:[{required:true,message:"请输入用户名",trigger:'blur'}],
  name:[{required:true,message:"请输入昵称",trigger:'blur'}],
  userpwd:[{required:true,message:"请输入密码",trigger:'blur'}]
})
const ruleFormRef = ref<FormInstance>()

const submitForm =async (ruleFormRef: FormInstance | undefined)=>{
  if (!ruleFormRef)return;
  await ruleFormRef.validate(async (valid,fields)=>{
    if(valid){
      let res = await Regist(ruleForm.name,ruleForm.username,ruleForm.userpwd)
      if (res){
        router.push({path:"/login"})
        ElMessage.success("注册成功")
      }
    }else {
      let errors:string ="";
      fields?.username?.forEach(element=>{
        errors += element.message + ';'
      })
      fields?.name?.forEach(element=>{
        errors += element.message + ';'
      })
      fields?.userpwd?.forEach(element=>{
        errors += element.message + ';'
      })
      ElMessage({
        type:"warning",
        message:errors
      })
    }
  })
}
</script>

<template>
  <el-card>
    <div class="left">

    </div>
    <div class="right" style="display: flex">
      <el-form
          ref="ruleFormRef"
          style="max-width: 600px"
          :model="ruleForm"
          status-icon
          :rules="rules"
          label-width="auto"
          class="demo-ruleForm">
        <h1>登录</h1>
        <el-form-item label="昵称" prop="name">
          <el-input v-model="ruleForm.name" />
        </el-form-item>
        <el-form-item label="用户名" prop="username">
          <el-input v-model="ruleForm.username" />
        </el-form-item>
        <el-form-item label="密码" prop="userpwd">
          <el-input v-model="ruleForm.userpwd" type="password" autocomplete="off" />
        </el-form-item>
        <el-form-item style="display: flex">
          <el-button type="primary" @click="submitForm(ruleFormRef)">注册</el-button>
        </el-form-item>
      </el-form>
    </div>
  </el-card>
</template>

<style scoped>

</style>
