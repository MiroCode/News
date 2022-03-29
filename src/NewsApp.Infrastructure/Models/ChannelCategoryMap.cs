namespace NewsApp.Infrastructure.Models
{
    public class ChannelCategoryMap : BaseEntity
    {
        public string ChannelId { get; set; }
        public string CategoryId { get; set; }
        public string XmlPath { get; set; }
    }
}
