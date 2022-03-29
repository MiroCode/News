namespace NewsApp.Infrastructure.Models
{
    public class NewsCommentNPoint : BaseEntity
    {
        public string NewsId { get; set; }
        public string CommandText { get; set; }
        public int Point { get; set; }
    }
}
