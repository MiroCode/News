using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class ListNewsImageQueryRequest : IRequest<IEnumerable<ListNewsImageQueryResponse>>
    {
        public string NewsId { get; set; }
    }
}
