using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Service
{
    public class WriterInfoService : BaseService<WriterInfo>, IWriterInfoService
    {
        public WriterInfoService(IBaseRepository<WriterInfo> iBaseRepository) : base(iBaseRepository)
        {
        }
    }
}
