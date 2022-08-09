namespace Articles.Data
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Created { get; set; }
        public string Content { get; set; }

        // todo : Author FK
        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}