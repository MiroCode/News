using System;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class ListCategoryQueryResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DisplayOrder { get; set; }

    }
}