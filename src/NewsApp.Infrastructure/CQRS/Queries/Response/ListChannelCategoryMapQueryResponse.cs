
namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class ListChannelCategoryMapQueryResponse
    {
        public string Id { get; set; }
        public string ChannelId { get; set; }
        public string CategoryId { get; set; }
        public string XmlPath { get; set; }

        public string ChannelName { get; set; }
        public string CategoryName { get; set; }
    }
}