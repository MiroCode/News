namespace NewsApp.Infrastructure.Models
{
    public class NewsImage : BaseEntity
    {
        public string NewsId { get; set; }
        public int DisplayOrder { get; set; }
        public string ImagePath { get; set; }
    }
}
