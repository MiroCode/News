using System;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class ListNewsNewsTagMapQueryResponse
    {
        public string Id { get; set; }
        public string NewsId { get; set; }
        public string TagId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
