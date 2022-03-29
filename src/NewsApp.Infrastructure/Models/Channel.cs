namespace NewsApp.Infrastructure.Models
{
    public class Channel : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int DisplayOrder { get; set; }
        public string ImagePath { get; set; }

    }
}
