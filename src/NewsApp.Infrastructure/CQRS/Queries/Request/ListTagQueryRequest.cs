using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class ListTagQueryRequest : IRequest<IEnumerable<ListTagQueryResponse>>
    {
        public string NewsId { get; set; }
        public string Name { get; set; }
    }
}
