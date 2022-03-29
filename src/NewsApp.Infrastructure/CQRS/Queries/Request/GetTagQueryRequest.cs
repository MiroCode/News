using MediatR;
using NewsApp.Infrastructure.CQRS.Queries.Response;

namespace NewsApp.Infrastructure.CQRS.Queries.Request
{
    public class GetTagQueryRequest : IRequest<TagQueryResponse>
    {
        public string Id { get; set; }
    }
}
