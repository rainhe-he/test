using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.WebApi.Utility.ApiResult;
using SqlSugar;
using System.Reflection;


namespace MyBlog.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BlogNewsController : ControllerBase
    {
        private readonly IBlogNewsService _iBlogNewsService;

        public ISqlSugarClient _db;
        public BlogNewsController(IBlogNewsService iBlogNewsService,ISqlSugarClient db)
        {
            _iBlogNewsService = iBlogNewsService;
            _db = db;
        }

        //[HttpGet]
        //public string InitDateBase()
        //{
        //    string res = "OK";
        //    //不存在则创建
        //    _db.DbMaintenance.CreateDatabase();

        //    string nspace = "MyBlog.Model";
        //    Type[] ass = Assembly.LoadFrom(AppContext.BaseDirectory + "Model.dll").GetTypes().Where(p => p.Namespace == nspace).ToArray();
        //    _db.CodeFirst.SetStringDefaultLength(200).InitTables(ass);

        //    return res;
        //}

        //[HttpGet]
        //public async Task<ActionResult<ApiResult>> GetBlogNews()
        //{
        //    int id = Convert.ToInt32(User.FindFirst("Id").Value);
        //    var data = await _iBlogNewsService.QueryAsync(u=>u.WriterId == id);           
        //    if (data == null) return ApiResultHelper.Error("没有更多的文章");
        //    return ApiResultHelper.Success(data);
        //}
        [HttpPost]
        public async Task<ActionResult<ApiResult>> Create(string title, string content, int typeid)
        {
            //数据验证
            BlogNews blogNews = new BlogNews
            {
                BrowseCount = 0,
                Content = content,
                LikeCount = 0,
                Time = DateTime.Now,
                Title = title,
                TypeId = typeid,
                WriterId = Convert.ToInt32(User.FindFirst("Id").Value),
            };
            bool b = await _iBlogNewsService.CreateAsync(blogNews);
            if (!b) return ApiResultHelper.Error("添加失败，服务器发生错误");
            return ApiResultHelper.Success(blogNews);
        }
        [HttpDelete]
        public async Task<ActionResult<ApiResult>> Delete(int id)
        {
            bool b = await _iBlogNewsService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }

        [HttpDelete]
        public async Task<ActionResult<ApiResult>> DeleteS(string ids)
        {
            object[] id =  ids.Split(',');
            bool b = await _iBlogNewsService.DeleteSAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
        [HttpPut]
        public async Task<ActionResult<ApiResult>> Edit(int id, string title, string content, int typeid)
        {
            var blogNews = await _iBlogNewsService.FindAsync(id);
            if (blogNews == null) return ApiResultHelper.Error("没有找到该文章");
            blogNews.Title = title;
            blogNews.Content = content;
            blogNews.TypeId = typeid;
            bool b = await _iBlogNewsService.EditAsync(blogNews);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(blogNews);
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<ApiResult> GetBlogNewsPage([FromServices] IMapper iMapper, int page, int size)
        {
            RefAsync<int> total = 0;
            var blognews = await _iBlogNewsService.QueryAsync(page, size, total);
            try
            {
                var blognewsDTO = iMapper.Map<List<BlogNewsDTO>>(blognews);
                return ApiResultHelper.Success(blognewsDTO, total);
            }
            catch (Exception)
            {
                return ApiResultHelper.Error("AutoMapper映射错误");
            }
        }

        [HttpGet]
        public async Task<ApiResult> GetUserBlogs(int page, int size , string? title)
        {
            RefAsync<int> total = 0;
            var data = new List<BlogNews>();
            int id = Convert.ToInt32(User.FindFirst("Id").Value);
            if(title.IsNullOrEmpty())
            {
                data = await _iBlogNewsService.QueryAsync(u => u.WriterId == id, page, size, total);
            }
            else
            {
                data = await _iBlogNewsService.QueryAsync(u => u.WriterId == id && u.Title.Contains(title), page, size, total);
            }
            if (data == null) return ApiResultHelper.Error("没有更多的文章");
            return ApiResultHelper.Success(data, total);
        }
    }
}
