import instance from './filter.ts'
const http ="/api"

//登录
export const getToken = (username:string,userpwd:string) => {
    return instance.post(http + "/Authoize/Login?username=" + username + "&userpwd=" + userpwd);
}
//注册
export const Regist = (name:string,username:string,userpwd:string) => {
    return instance.post(http + "/Writer/Create?name=" + name + "&username=" + username + "&userpwd=" + userpwd);
}

//获取全部blog
export const getBlogNew = (page:number,size:number)=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.get(http + "/BlogNews/GetBlogNewsPage?page="+ page +"&size=" +size)
}

//获取个人blog
export const getUBlogNew = (page:number,size:number,title:string)=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.get(http + "/BlogNews/GetUserBlogs?page="+ page +"&size=" +size+"&title=" +title)
}

//删除博客信息
export const DeleteBlogInfo = (id:number)=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.delete(http + "/BlogNews/Delete?id="+ id)
}

//批量删除博客信息
export const DeleteSBlogInfo = (id:number[])=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.delete(http + "/BlogNews/DeleteS?ids=" + id )
}

//修改个人信息
export const EditUserInfo = (name:string)=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.put(http + "/Writer/Edit?name="+ name)
}

//获取博客类型
export const GetBlogType = ()=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.get(http + "/Type/Types")
}

//创建博客
export const CreateBlog = (title:string,content:string,typeId?:number)=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.post(http + "/BlogNews/Create?title=" + title + "&content=" + content + "&typeid=" + typeId )
}

//编辑博客
export const EditBlog = (id:number,title:string,content:string,typeId?:number)=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.put(http + "/BlogNews/Edit?id="+ id +"&title=" + title + "&content=" + content + "&typeid=" + typeId )
}

//获取用户
export const getWritter = (page:number,size:number,name:string)=>{
    instance.defaults.headers.common['Authorization'] = "Bearer " + localStorage["Token"];
    return instance.get(http + "/Writer/GetWriters?page="+ page +"&size=" +size+"&Name=" + name )
}

