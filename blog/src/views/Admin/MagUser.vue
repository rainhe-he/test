<script setup lang="ts">
import {onMounted, reactive, ref} from "vue";
import {ElMessage, ElTable, FormInstance, FormRules} from "element-plus";
import {DeleteBlogInfo, DeleteSBlogInfo, getUBlogNew, getWritter} from "../../axios/http.ts";
import Add_EditBlog from "../../components/User/Add_EditBlog.vue";
import {UserBlog} from "../../components/User/class/UserBlog.ts";
import {Search,Refresh,Timer} from "@element-plus/icons-vue"
import Add_User from "../../components/Admin/Add_User.vue";

const form = reactive({
  name:"",
  userName:"",
  pageIndex:1,
  pageSize:5,
  Total:0
})

const tableData = ref<Array<object>>()
const onSubmit = async () =>{
  await LoadTableData()
}
const resetForm = () =>{
  form.name = ""
  LoadTableData()
}

const isShow = ref(false)
const info = ref<UserBlog>(new UserBlog())

const eidtVal =async (row:UserBlog) =>{
  console.log(row)
  info.value = row
  isShow.value =true
}

const openAdd = () => {
  isShow.value = true
}

const multipTableTef = ref()
//批量删除
const onDel = async () => {
  let rows = multipTableTef.value?.getSelectionRows()
  //let res = await DeleteSBlogInfo(rows.map(item =>{return `${item.id}`}).join(","))
  //if (res.code == 200){
  //   ElMessage.success("删除成功")
  //   await LoadTableData()
  // }

}

//单个删除
const deleteVal = async(row:object) =>{
  //let res = await DeleteBlogInfo(row.id) as any
  LoadTableData()
//   if (res.code == 200){
//     ElMessage.success("删除成功")
//   }
}

onMounted(async () =>{
  await LoadTableData()
})

const LoadTableData = async () =>{
  let res = await getWritter(form.pageIndex,form.pageSize,form.name) as any
  console.log(res)
  tableData.value =res.data
  form.Total = res.total
}

const handleCurrentChange = (val:number) =>{
  form.pageIndex = val
  LoadTableData()
}

const closeAdd =()=>{
  isShow.value = false
  info.value = new UserBlog()
}
const success = async ()=>{
  isShow.value = false
  info.value = new UserBlog()
  await LoadTableData()
}
</script>

<template>
  <el-card class="box-card">
    <template #header>
      <div class="card-header">
        <el-form :inline="true" :model="form" >
          <el-form-item label="用户名称" prop="name">
            <el-input v-model="form.name" placeholder="请输入用户名称"></el-input>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="onSubmit">
              <el-icon>
                <search></search>
              </el-icon>查询
            </el-button>
          </el-form-item>
          <el-form-item>
            <el-button type="primary" @click="resetForm">
              <el-icon>
                <refresh></refresh>
              </el-icon>重置
            </el-button>
          </el-form-item>
        </el-form>
      </div>
    </template>
    <p>
      <el-button type="primary" @click="openAdd">新增</el-button>
      <el-button type="danger" @click="onDel">批量删除</el-button>
    </p>
    <div class="container">

      <el-table :data="tableData"  style="width: 100%" ref="multipTableTef" row-key="id">
        <el-table-column type="selection" width="55" />
        <el-table-column prop="id" label="序号" width="60" />
        <el-table-column  label="昵称" width="200">
          <template #default="scope">
            <div>{{scope.row.name}}</div>
          </template>
        </el-table-column>
        <el-table-column  label="用户名" width="300">
          <template #default="scope">
            <div>{{scope.row.userName}}</div>
          </template>
        </el-table-column>
        <el-table-column fixed="right" label="操作" width="120">
          <template #default="scope">
            <el-button link type="primary" size="small" @click="eidtVal(scope.row)">编辑</el-button>
            <el-button link type="primary" size="small" @click="deleteVal(scope.row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>
    <div class="example-pagination-block">
      <el-pagination background layout="prev, pager, next" :total="form.Total" :page-size="form.pageSize" @current-change="handleCurrentChange"/>
    </div>
  </el-card>
  <add_-user :dialog-visible="isShow" :info="info" @close-add="closeAdd" @success="success"></add_-user>
</template>

<style scoped>

</style>
