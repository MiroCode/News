using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class ListUserQueryRequest : IRequest<IEnumerable<ListUserQueryResponse>>
    {
        public string Email { get; set; }
    }
}
