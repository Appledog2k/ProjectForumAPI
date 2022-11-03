using Articles.Models.BaseModels;
using Articles.Models.Data.AggregateArticles;

namespace Articles.Models.Data.AggregateImages
{
    public class ImageCore : BaseModel
    {
        /// <summary>
        /// Đường dẫn tới ảnh
        /// </summary>
        public string ImagePath { get; set; }
        /// <summary>
        /// Nội dung của ảnh
        /// </summary>
        public string Caption { get; set; }
    }

}