using Articles.Models.Data.AggregateUsers;
using Articles.Models.BaseModels;
using Articles.Models.Data.AggregateArticles;
using System.ComponentModel.DataAnnotations;
using Articles.Models.Data.AggregateCategories;

namespace Articles.Models.Data.AggregateArticleInCategory
{
    public class ArticleInCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}