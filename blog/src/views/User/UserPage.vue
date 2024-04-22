<script setup lang="ts">
import {
  Document,
  Menu as IconMenu,
  Setting,
} from '@element-plus/icons-vue'
</script>

<template>
  <div class="common-layout">
    <el-container>
      <el-aside width="200px">
        <el-menu
            active-text-color="#ffd04b"
            background-color="#545c64"
            class="el-menu-vertical-demo"
            default-active="2"
            text-color="#fff"
            style="height: 100vh"
            router>
          <el-menu-item index="blogPage">
            <el-icon><icon-menu /></el-icon>
            <span>博客页面</span>
          </el-menu-item>
          <el-menu-item index="userInfo" >
            <el-icon><document /></el-icon>
            <span>修改个人信息</span>
          </el-menu-item>
          <el-menu-item index="UBlogInfo">
            <el-icon><setting /></el-icon>
            <span>编辑个人博客</span>
          </el-menu-item>
        </el-menu>
      </el-aside>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </div>
</template>

<style scoped>

</style>
