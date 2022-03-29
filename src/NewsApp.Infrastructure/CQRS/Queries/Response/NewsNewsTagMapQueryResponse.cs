using System;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class NewsNewsTagMapQueryResponse
    {
        public string NewsId { get; set; }
        public string TagId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
