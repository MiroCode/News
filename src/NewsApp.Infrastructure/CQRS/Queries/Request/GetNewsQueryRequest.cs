using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetNewsQueryRequest : IRequest<NewsQueryResponse>
    {
        public string Id { get; set; }
    }
}
