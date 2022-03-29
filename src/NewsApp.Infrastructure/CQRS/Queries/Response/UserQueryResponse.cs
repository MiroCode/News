using System;

namespace NewsApp.Infrastructure.CQRS.Queries.Response
{
    public class UserQueryResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string AppId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
