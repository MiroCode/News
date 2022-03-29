using System;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class ListUserInterestQueryResponse
    {
        public string Id { get; set; }
        public string ChannelCategoryMapId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
