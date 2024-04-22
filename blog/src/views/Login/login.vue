<script setup lang="ts">
import {reactive, ref} from "vue";
import {ElMessage, FormInstance, FormRules} from "element-plus";
import router from "../../router";
import {getToken} from "../../axios/http.ts";
import store from "../../vuex/vuex.ts";
import {Userinfo} from "./class/Userinfo.ts";


const ruleForm = reactive({
  username:"",
  pass:""
})
const rules = reactive<FormRules>({
  username:[{required:true,message:"请输入用户名",trigger:'blur'}],
  pass:[{required:true,message:"请输入密码",trigger:'blur'}]
})
const ruleFormRef = ref<FormInstance>()

const submitForm =async (ruleFormRef: FormInstance | undefined)=>{
  if (!ruleFormRef)return;
  await ruleFormRef.validate(async (valid,fields)=>{
    if(valid){
      const token = (await getToken(ruleForm.username, ruleForm.pass)).data
      const user:Userinfo=JSON.parse(decodeURIComponent(escape(window.atob(token.replace("-","+").replace("_","/").split('.')[1]))))
      localStorage["Token"] = token
      localStorage["Name"] = user.Name
      store.commit("SettingName",user.Name)
      store.commit("SettingToken",token)
      router.push({path:"/blogPage"})
    }else {
      console.log(fields)
      let errors:string ="";
      fields?.username?.forEach(element=>{
        errors += element.message + ';'
      })
      fields?.pass?.forEach(element=>{
        errors += element.message + ';'
      })
      ElMessage({
        type:"warning",
        message:errors
      })
    }
  })
}

const registUser = () => {
  router.push({path:"/regist"})
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
          <el-form-item label="用户名" prop="username">
            <el-input v-model="ruleForm.username" />
          </el-form-item>
          <el-form-item label="密码" prop="pass">
            <el-input v-model="ruleForm.pass" type="password" autocomplete="off" />
          </el-form-item>
          <el-form-item style="display: flex">
            <el-button type="primary" @click="submitForm(ruleFormRef)">登录</el-button>
            <el-button type="primary" @click="registUser">注册</el-button>
          </el-form-item>
        </el-form>
      </div>
    </el-card>
</template>

<style scoped>

</style>
