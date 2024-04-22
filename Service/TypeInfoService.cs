﻿using MyBlog.IRepository;
using MyBlog.IService;
using MyBlog.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyBlog.Service
{
    public class TypeInfoService : BaseService<TypeInfo>, ITypeInfoService
    {
        public TypeInfoService(IBaseRepository<TypeInfo> iBaseRepository) : base(iBaseRepository)
        {
        }
    }
}
