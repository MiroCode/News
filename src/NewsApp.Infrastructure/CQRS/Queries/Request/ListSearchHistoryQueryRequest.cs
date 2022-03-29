using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;
using System.Collections.Generic;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class ListSearchHistoryQueryRequest : IRequest<IEnumerable<ListSearchHistoryQueryResponse>>
    {
        public string SearchText { get; set; }
    }
}
