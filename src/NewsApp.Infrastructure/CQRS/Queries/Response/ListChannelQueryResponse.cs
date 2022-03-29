using System;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class ListChannelQueryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DisplayOrder { get; set; }

    }
}