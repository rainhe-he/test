<script setup lang="ts">
import {computed, onMounted, ref, Ref, watch} from "vue";
import {UserBlog} from "./class/UserBlog.ts";
import {CreateBlog, EditBlog, GetBlogType} from "../../axios/http.ts";
import {ElMessage} from "element-plus";
const props = defineProps({
  dialogVisible:Boolean,
  info:UserBlog
})

const form:Ref<UserBlog> = ref<UserBlog>({
  title:"",
  content:"",
  typeId:null,
  id:null
})

watch(()=>props.info,(newInfo) =>{
  if (newInfo){
    form.value={
      title:newInfo.title,
      content: newInfo.content,
      typeId:newInfo.typeId,
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
  if(!form.value.id){
    let res = await CreateBlog(form.value.title,form.value.content,form.value.typeId)
    if (res.code == 200){
      ElMessage.success("添加成功")
    }
  }else {
    let res = await EditBlog(form.value.id,form.value.title,form.value.content,form.value.typeId)
    if (res.code == 200){
      ElMessage.success("修改成功")
    }
  }
  emits("success")
}

onMounted(
    async () =>{
      LoadType()
    }
)

const LoadType = async () =>{
  let res = await GetBlogType() as any
  BlogType.value = res.data
}
</script>

<template>
  <el-dialog v-model="dialogVisible" :title="info?.title? '修改' : '编辑' " width="30%" @close="$emit('closeAdd')" draggable>
    <el-form>
      <el-form-item label="标题" prop="date">
        <el-input v-model="form.title"></el-input>
      </el-form-item>
      <el-form-item label="博客内容" prop="name">
        <el-input
            v-model="form.content"
            style="width: 240px"
            :autosize="{ minRows: 2, maxRows: 4 }"
            type="textarea"
            placeholder="Please input"
        ></el-input>
      </el-form-item>
      <el-form-item label="博客类型" prop="address">
        <el-select v-model="form.typeId">
          <el-option v-for="item in BlogType" :label="item.name" :value="item.id"></el-option>
        </el-select>
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
