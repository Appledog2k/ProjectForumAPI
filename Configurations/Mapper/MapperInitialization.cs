using Articles.Data;
using Articles.Models;
using Articles.Models.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Articles.Configurations.Mapper
{
    public class MapperInitialization : Profile
    {
        public MapperInitialization()
        {
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<Article, Create_ArticleDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, Create_AuthorDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}