
using Articles.Models.Data.AggregateArticles;

namespace Articles.Models.Data.AggregateImages
{
    public class ImageArticle : ImageCore
    {
        /// <summary>
        /// Id bài viết chứa ảnh
        /// </summary>
        public int ArticleId { get; set; }
        /// <summary>
        /// Bài viết
        /// </summary>
        public Article Article { get; set; }
    }

}