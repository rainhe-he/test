using AutoMapper;
using MyBlog.Model;
using MyBlog.Model.DTO;

namespace WebApplication1.Config
{
    public class AutoMapperConfigs:Profile
    {
        public AutoMapperConfigs() 
        {
            CreateMap<WriterInfo, WriterDTO>();
            CreateMap<BlogNews, BlogNewsDTO>()
            .ForMember(dest => dest.TypeName, sourse => sourse.MapFrom(src => src.TypeInfo.Name))
            .ForMember(dest => dest.WriterName, sourse => sourse.MapFrom(src => src.WriterInfo.Name));
        }
    }
}
