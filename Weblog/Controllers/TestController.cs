using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.WebApi.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class TestController : ControllerBase
  {
    [HttpGet]
    public string NoAuthorize()
    {
      return "this is NoAuthorize";
    }
    
    [HttpGet]
    [Authorize]
    public string Authorize()
    {
      return "this is Authorize";
    }
  }
}
