using MyBlog.IRepository;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Repository
{
  public class WriterInfoRepository : BaseRepository<WriterInfo>, IWriterInfoRepository
  {
        public WriterInfoRepository(ISqlSugarClient db) : base(db)
        {
        }

  }
}
