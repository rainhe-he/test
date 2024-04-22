<script setup lang="ts">
import {computed, onMounted, ref, Ref, watch} from "vue";
import {User} from "./class/User.ts";
import {CreateBlog, EditBlog, GetBlogType} from "../../axios/http.ts";
import {ElMessage} from "element-plus";
const props = defineProps({
  dialogVisible:Boolean,
  info:User
})

const form:Ref<User> = ref<User>({
  name:"",
  userName:"",
  userPwd:"",
  id:null
})

watch(()=>props.info,(newInfo) =>{
  if (newInfo){
    form.value={
      name:newInfo.name,
      userName: newInfo.userName,
      userPwd:newInfo.userPwd,
      id:newInfo.id
    }
  }
})

const BlogType = ref()
const dialogVisible = computed(()=>props.dialogVisible)

const  emits = defineEmits(["closeAdd","success"])
const  closeAdd = () =>{
  emits("closeAdd")
}
const save = async ()=>{
  // if(!form.value.id){
  //   let res = await CreateBlog(form.value.title,form.value.content,form.value.typeId)
  //   if (res.code == 200){
  //     ElMessage.success("添加成功")
  //   }
  // }else {
  //   let res = await EditBlog(form.value.id,form.value.title,form.value.content,form.value.typeId)
  //   if (res.code == 200){
  //     ElMessage.success("修改成功")
  //   }
  // }
  emits("success")
}
</script>

<template>
  <el-dialog v-model="dialogVisible" :title="info?.name? '修改' : '编辑' " width="30%" @close="$emit('closeAdd')" draggable>
    <el-form>
      <el-form-item label="昵称">
        <el-input v-model="form.name"></el-input>
      </el-form-item>
      <el-form-item label="用户名">
        <el-input v-model="form.userName"></el-input>
      </el-form-item>
      <el-form-item label="密码">
          <el-input v-model="form.userPwd"></el-input>
      </el-form-item>
    </el-form>
    <template #footer>
    <span class="dialog-footer">
      <el-button @click="closeAdd">取消</el-button>
      <el-button type="primary" @click="save">保存</el-button>
    </span>
    </template>
  </el-dialog>
</template>

<style scoped>

</style>
