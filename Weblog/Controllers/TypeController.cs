using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBlog.IService;
using MyBlog.Model;
using MyBlog.Service;
using MyBlog.WebApi.Utility.ApiResult;

namespace Weblog.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TypeController : ControllerBase
    {
        private readonly ITypeInfoService _iTypeInfoService;

        public TypeController(ITypeInfoService iTypeInfoService)
        {
            _iTypeInfoService = iTypeInfoService;
        }

        [HttpGet]
        public async Task<ApiResult> Types()
        {
            var types = await _iTypeInfoService.QueryAsync();
            if (types.Count == 0) return ApiResultHelper.Error("没有更多的类型");
            return ApiResultHelper.Success(types);
        }
        [HttpPost]
        public async Task<ApiResult> Create(string name)
        {
            #region 数据验证
            if (String.IsNullOrWhiteSpace(name)) return ApiResultHelper.Error("文章类型名不能为空");
            #endregion
            TypeInfo type = new TypeInfo
            {
                Name = name
            };
            bool b = await _iTypeInfoService.CreateAsync(type);
            if (!b) return ApiResultHelper.Error("添加失败");
            return ApiResultHelper.Success(b);
        }
        [HttpPut]
        public async Task<ApiResult> Edit(int id, string name)
        {
            var type = await _iTypeInfoService.FindAsync(id);
            if (type == null) return ApiResultHelper.Error("没有找到该文章类型");
            type.Name = name;
            bool b = await _iTypeInfoService.EditAsync(type);
            if (!b) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success(type);
        }
        [HttpDelete]
        public async Task<ApiResult> Delete(int id)
        {
            bool b = await _iTypeInfoService.DeleteAsync(id);
            if (!b) return ApiResultHelper.Error("删除失败");
            return ApiResultHelper.Success(b);
        }
    }
}
