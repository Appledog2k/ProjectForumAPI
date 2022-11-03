using Articles.Models.Data.AggregateArticles;
using Articles.Models.Data.AggregateUsers;
using Articles.Models.DTOs;
using AutoMapper;
namespace Articles.Configurations.Mapper
{
    public class MapperInitialization : Profile
    {
        public MapperInitialization()
        {
            /// Mapper Article
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<Article, Create_ArticleDTO>().ReverseMap();
            CreateMap<Article, Update_ArticleDTO>().ReverseMap();

            /// Mapper User
            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}