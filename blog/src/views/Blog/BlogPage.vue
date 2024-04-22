<script setup lang="ts">
import {Blog} from "../../components/Blog/class/Blog.ts";
import {onMounted, reactive, ref} from "vue";
import {getBlogNew} from "../../axios/http.ts";
import Blogbox from "../../components/Blog/Blogbox.vue";
import router from "../../router";

const Page = reactive({
  data:[],
  pageIndex:1,
  pageSize:5,
  Total:0
})

const Name = localStorage["Name"]

const LoadTableData = async (name: string ="") =>{
  let res = (await getBlogNew(Page.pageIndex, Page.pageSize)) as any
  Page.Total = res.total
  Page.data = res.data
}

onMounted(async () =>{
  LoadTableData()
})

const handleCurrentChange = (val:number) =>{
  Page.pageIndex = val
  LoadTableData()
}

const Loginout =() =>{
  //localStorage["Token"] = ""
  //localStorage["Name"] = ""
  router.push({path:"/login"})
}

const UserCenter = () => {
  router.push({path:"/userInfo"})
}

</script>

<template>
  <el-header >
    <el-page-header :icon="null" style="border-bottom: 1px solid #ccc">
      <template #content>
          <el-dropdown>
            <el-avatar
                :size="32"
                class="mr-3"
                src=""/>
            <template #dropdown>
              <el-dropdown-menu>
                <el-dropdown-item @click="Loginout">切换</el-dropdown-item>
                <el-dropdown-item @click="UserCenter">个人中心</el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
        <div style="float: right">{{Name}}</div>
      </template>
    </el-page-header>
  </el-header>
  <el-main>
    <el-card v-for="item in Page.data">
        <Blogbox :info="item"></Blogbox>
    </el-card>
  </el-main>
  <el-footer>
    <div class="example-pagination-block">
        <el-pagination background layout="prev, pager, next" :total="Page.Total" :page-size="Page.pageSize" @current-change="handleCurrentChange"/>
    </div>
  </el-footer>
</template>

<style scoped>

</style>
