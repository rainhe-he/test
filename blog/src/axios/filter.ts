import axios from "axios";
import {ElMessage}  from "element-plus";
//创建一个axios实例
const instance = axios.create({
    headers:{
        'content-type':'application/json',
    },
    //withCredentials:true,
    timeout:5000
})
//http拦截器
instance.interceptors.response.use(
    response=>{
        if (response.data.code == 200){
            return response.data
        }else {
            ElMessage.warning(response.data.msg)
        }
    },
    //
    error => {
        if(error.response.data){
            switch (error.response.status){
                case 401:
                    ElMessage.warning("资源没有访问权限")
                    break
                case 404:
                    ElMessage.warning("接口不存在")
                    break
                case 500:
                    ElMessage.warning("内部服务器错误请联系管理员")
                    break
                default:
                    return Promise.reject(error.response.data)
            }
        }
        else if(JSON.stringify(error).indexOf("timeout")>-1){
            ElMessage.error("接口超时")
        }else{
            ElMessage.error("跨域错误")
        }

    }
)


export  default  instance
