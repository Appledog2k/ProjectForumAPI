using Articles.Data;
using Articles.Models;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;


namespace Articles.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleContext _context;
        private readonly IMapper _mapper;
        public ArticleRepository(ArticleContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        // get all article
        public async Task<List<ArticleModel>> GetAllArticlesAsync()
        {
            var result = await _context.Articles.ToListAsync();
            return _mapper.Map<List<ArticleModel>>(result);

        }

        // get article by id
        public async Task<ArticleModel> GetArticleByIdAsync(int articleId)
        {
            // var result = await _context.Articles.Where(x => x.Id == articleId).Select(x => new ArticleModel()
            // {
            //     Id = x.Id,
            //     Title = x.Title,
            //     Created = x.Created,
            //     Content = x.Content
            // }).FirstOrDefaultAsync();
            // return result;
            var result = await _context.Articles.FindAsync(articleId);
            return _mapper.Map<ArticleModel>(result);
        }
        // add article or create new article
        public async Task<int> AddArticleAsync(ArticleModel articleModel)
        {
            var article = new Article()
            {
                Title = articleModel.Title,
                Content = articleModel.Content,
                Created = articleModel.Created
            };
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return article.Id;

        }
        // update article
        public async Task UpdateArticleAsync(int articleId, ArticleModel articleModel)
        {
            // var article = await _context.Articles.FindAsync(articleId);
            // if (article != null)
            // {
            //     article.Title = articleModel.Title;
            //     article.Content = articleModel.Content;
            //     article.Created = articleModel.Created;
            //     await _context.SaveChangesAsync();

            // }
            var article = new Article()
            {
                Id = articleId,
                Title = articleModel.Title,
                Created = articleModel.Created,
                Content = articleModel.Content


            };
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateArticlePatchAsync(int articleId, JsonPatchDocument articleModel)
        {
            var article = await _context.Articles.FindAsync(articleId);
            if (article != null)
            {
                articleModel.ApplyTo(article);
                await _context.SaveChangesAsync();
            }
        }


        // delete article
        public async Task DeleteArticleAsync(int articleId)
        {
            var article = new Article() { Id = articleId };
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }


    }
}
