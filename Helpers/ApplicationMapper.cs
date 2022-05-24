using Articles.Data;
using Articles.Models;
using Articles.Models.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Articles.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Article, ArticleDTO>().ReverseMap();
            CreateMap<Article, Create_ArticleDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Author, Create_AuthorDTO>().ReverseMap();
            CreateMap<ApiUser, UserDTO>().ReverseMap();


        }
    }
}