using Articles.Models.Data.AggregateUsers;
using Articles.Models.BaseModels;

namespace Articles.Models.Data.AggregateArticles
{
    public class Article : BaseModel
    {
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
    }
}