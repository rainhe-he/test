import {createRouter,createWebHistory} from "vue-router";

const router = createRouter({
    history:createWebHistory(),
    routes:[
        {path:"/",redirect:'/Admin'},
        {name:"LogPage",path:"/logPage",component: () => import ("../views/Login/LogPage.vue"),
            children:[
                {name:"login",path:"/login",component: () => import ("../views/Login/login.vue")},
                {name:"regist",path:"/regist",component: () => import ("../views/Login/regist.vue")},
            ]},
        {name:"BlogPage",path:"/blogPage",component: () => import ("../views/Blog/BlogPage.vue")},
        {name:"UserPage",path:"/userPage",redirect:'/UBlogInfo',component: () => import ("../views/User/UserPage.vue"),
            children:[
                {name:"UserInfo",path:"/userInfo",component: () => import ("../views/User/Userinfo.vue")},
                {name:"UserBlogInfo",path:"/UBlogInfo",component: () => import ("../views/User/UBloginfo.vue")}
            ]},
        {name:"AdminPage",path:"/Admin",redirect:'/magUser',component: () => import ("../views/Admin/AdminPage.vue"),
        children:[
            {name:"MgUser",path:"/magUser",component: () => import ("../views/Admin/MagUser.vue")}
        ]},
    ]
})

export default router
