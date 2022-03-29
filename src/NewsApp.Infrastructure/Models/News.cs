namespace NewsApp.Infrastructure.Models
{
    public class News : BaseEntity
    {
        public string ProviderNewsId { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }
        public string XmlPath { get; set; }
        public int DisplayOrder { get; set; }
        public string ChannelCategoryMapId { get; set; }
    }
}
