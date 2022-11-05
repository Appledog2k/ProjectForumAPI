using Articles.Models.Data.AggregateArticles;
using Articles.Models.Data.AggregateUsers;
using Articles.Models.DTOs;
using Articles.Models.DTOs.ArticleImage;
using AutoMapper;
namespace Articles.Configurations.Mapper
{
    public class MapperInitialization : Profile
    {
        public MapperInitialization()
        {
            /// Mapper Article
            CreateMap<Article, ArticleViewRequest>().ReverseMap();
            CreateMap<Article, ArticleCreateRequest>().ReverseMap();
            CreateMap<Article, ArticleUpdateRequest>().ReverseMap();

            /// Mapper User
            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}