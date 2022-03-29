using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class ListUserInterestQueryRequest : IRequest<IEnumerable<ListUserInterestQueryResponse>>
    {
        public string UserId { get; set; }
    }
}
