using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetSearchHistoryQueryRequest : IRequest<SearchHistoryQueryResponse>
    {
        public string Id { get; set; }
    }
}
