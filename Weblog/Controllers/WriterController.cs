using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Model.DTO;
using MyBlog.Service;
using MyBlog.WebApi.Utility._MD5;
using MyBlog.WebApi.Utility.ApiResult;
using SqlSugar;

namespace Weblog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class WriterController : ControllerBase
    {
        private readonly IWriterInfoService _iWriterInfoService;
        public WriterController(IWriterInfoService iWriterInfoService)
        {
            _iWriterInfoService = iWriterInfoService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ApiResult> Create(string name, string username, string userpwd)
        {
            //数据校验
            WriterInfo writer = new WriterInfo
            {
                Name = name,
                //加密密码
                UserPwd = MD5Helper.MD5Encrypt32(userpwd),
                UserName = username
            };
            //判断数据库中是否已经存在账号跟要添加的账号相同的数据
            var oldWriter = await _iWriterInfoService.FindAsync(c => c.UserName == username);
            if (oldWriter != null) return ApiResultHelper.Error("用户名已被注册");

            bool b = await _iWriterInfoService.CreateAsync(writer);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(writer);
        }
        [HttpPut]
        public async Task<ApiResult> Edit(string name)
        {
            int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
            var writer = await _iWriterInfoService.FindAsync(id);
            writer.Name = name;
            bool b = await _iWriterInfoService.EditAsync(writer);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ApiResult> FindWriter([FromServices] IMapper iMapper, int id)
        {
            var writer = await _iWriterInfoService.FindAsync(id);
            var writerDTO = iMapper.Map<WriterDTO>(writer);
            return ApiResultHelper.Success(writerDTO);
        }

        //[HttpGet]
        //public async Task<ApiResult> FindWriterALL()
        //{
        //    int id = Convert.ToInt32(this.User.FindFirst("Id").Value);
        //    var writer = await _iWriterInfoService.FindAsync(id);
        //    return ApiResultHelper.Success(writer);
        //}

        [HttpGet]
        public async Task<ApiResult> GetWriters(int page, int size, string? Name)
        {
            RefAsync<int> total = 0;
            var data = new List<WriterInfo>();
            if (Name.IsNullOrEmpty())
            {
                data = await _iWriterInfoService.QueryAsync(page, size, total);
            }
            else
            {
                data = await _iWriterInfoService.QueryAsync(u=>u.Name.Contains(Name), page, size, total);
            }
            if (data == null) return ApiResultHelper.Error("没有更多的作者");
            return ApiResultHelper.Success(data, total);
        }
    }
}
