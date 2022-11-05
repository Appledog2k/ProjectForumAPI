using Articles.Models.Data.AggregateUsers;
using Articles.Models.BaseModels;
using Articles.Models.Data.AggregateCategories;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Articles.Models.Data.AggregateArticleInCategory;

namespace Articles.Models.Data.AggregateArticles
{
    public class Article
    {
        [Comment("Id bảng, khóa chính")]
        [Key]
        public int Id { get; set; }
        [Comment("Ngày tạo")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Tiêu đề bài viết
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Nội dung bài viết
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Số lượt xem bài viết
        /// </summary>
        public int ViewCount { get; set; }
        /// <summary>
        /// Id người tạo
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// Người tạo bài viết
        /// </summary>
        public ApiUser ApiUser { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string AuthorName { get; set; }
        /// <summary>
        // Dánh sách ảnh
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual IList<ArticleInCategory> ArticleInCategories { get; set; }

    }
}