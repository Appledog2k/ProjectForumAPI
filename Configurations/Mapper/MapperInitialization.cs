using Articles.Data;
using Articles.Models.DTOs;
using AutoMapper;
namespace Articles.Configurations.Mapper
{
    public class MapperInitialization : Profile
    {
        public MapperInitialization()
        {
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<Article, Create_ArticleDTO>().ReverseMap();
            CreateMap<Article, Update_ArticleDTO>().ReverseMap();

            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}