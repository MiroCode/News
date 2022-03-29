using System;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class ListNewsImageQueryResponse
    {
        public string Id { get; set; }
        public string NewsId { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DisplayOrder { get; set; }

    }
}
